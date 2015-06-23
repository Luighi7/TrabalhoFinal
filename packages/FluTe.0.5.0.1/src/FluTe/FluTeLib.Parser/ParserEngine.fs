module internal FluTeLib.Parser.ParserEngine
open FluTeLib.Parser.LexClasses
open FParsec
open FParsec.CharParsers
open FParsec.Primitives
open FluTeLib
open FluTeLib.Core.Token
open FluTeLib.Core.Input
open System
open FluTeLib.Core.Template
open System.Linq;
let (|?) left right = (fun x -> left x || right x)
let contains (lst : 'a list) (x : 'a) = lst.Contains(x)

type FirstCharInInput = 
    | Normal of char
    | Scope of char

type MemberIdentifier = 
    | Simple of string
    | Compound of string * string list
    override this.ToString() = 
        match this with
        | Simple(name) -> name
        | Compound(name, inputs) -> String.Join(".", name::inputs)

let FluTeParser =
    let raiseInvalidTokenStructure tokenName = raise (InvalidTemplateException(sprintf "Invalid token structure in '%s'" tokenName))
    let raiseDuplicateInputNames tokenName = raise (InvalidTemplateException(sprintf "Input name conflict in '%s'" tokenName))
    let raiseConflictingTokens tokenName = raise (InvalidTemplateException(sprintf "Token name conflict for '%s'" tokenName))
    
    //define some basic parsers
    let pInjStart = skipAnyOf Symbols.Inj.Start
    let pInjEnd = skipAnyOf Symbols.Inj.End
    let pEscapeChar = skipAnyOf Symbols.Escape
    let cgNonLiteralChars = Symbols.Inj.Start @ Symbols.Escape
    let pLiteralToken = 
        many1Satisfy (not << cgNonLiteralChars.Contains) 
        |>> Tokens.DefineLiteral

    let pEscape = 
        anyOf Symbols.Escape 
        >>. (anyOf cgNonLiteralChars <|> preturn '`') 
        |>> (string >> Tokens.DefineLiteral) 
    
    let pAnyInjectionToken = //here we define the token parser
        //we define the simple parsers for the separators
        let pSepTokenAndInput = spaces >>. anyOf Symbols.Sep.TokenAndInput .>> spaces
        let pSepInputAndInput= spaces >>. anyOf Symbols.Sep.InputAndInput .>> spaces
        let pSepIdentAndMember = spaces >>. anyOf Symbols.Sep.IdentAndMember .>> spaces
        let pPredefinedInputMark = spaces >>. anyOf Symbols.PredefinedInputMark .>> spaces
        let pIdentifier = many1Satisfy (isLetter |? isDigit)
        let defineMember (ident : string) (met : string option) = 
            InputMemberReference(ident, if met.IsSome then MemberType.MethodNoArgs else MemberType.PropertyOrField)

        let pMember = pipe2 (many1Satisfy (isLetter |? isDigit)) (opt (pstring "()")) defineMember

        let defineInput (isPredefined : bool) (name : string) (members : InputMemberReference list option)= 
            let rMembers = if members.IsSome then members.Value else List.empty
            let typ = if isPredefined then InputType.Static else InputType.Instance
            InputReference(InputKey(name, typ), rMembers)
            
        let pInput = pipe3 ((spaces >>. opt (anyOf Symbols.PredefinedInputMark) .>> spaces) |>> (fun x -> if x.IsSome then true else false))
                           (many1Satisfy (isLetter |? isDigit) .>> spaces)
                           (opt (pSepIdentAndMember >>. sepBy pMember pSepIdentAndMember))
                           defineInput
        
        let defineToken (startInputRef : InputReference) (restInputRefs : InputReference list option) =
            let realRestInputRefs = if restInputRefs.IsNone then List.empty else restInputRefs.Value 
            if realRestInputRefs.Any() && (startInputRef.Type.Equals(InputType.Static) || startInputRef.InvocationChain.Any())
                then failwith "Unknown..."
            Tokens.DefineInjection(startInputRef.Label, (if realRestInputRefs.Any() then realRestInputRefs else [startInputRef]) |> List.toSeq)

        pipe2 (pInjStart >>. spaces >>. pInput .>> spaces) (opt (pSepTokenAndInput >>. sepBy pInput pSepInputAndInput) .>> spaces .>> pInjEnd) defineToken
    many (pLiteralToken <|> pEscape <|> pAnyInjectionToken)
let raiseParserError message = raise(InvalidTemplateException(sprintf "Could not parse the template. \n%s" message))  
let Parse str =    
    match run FluTeParser str with 
    | Success(res, _, _) -> res
    | Failure(err, _, _) -> raiseParserError err
  