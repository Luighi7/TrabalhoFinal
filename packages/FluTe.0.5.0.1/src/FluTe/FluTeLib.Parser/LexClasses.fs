module internal FluTeLib.Parser.LexClasses
open FParsec.Primitives
open FParsec.CharParsers
type Parser<'u> = Parser<'u, unit>
type chlist = char list
type tyInj = {Start : chlist; End : chlist}
type tySep = {TokenAndInput : chlist; IdentAndMember : chlist; InputAndInput : chlist}
type tySym = {Inj : tyInj; Sep : tySep; Escape : char list; PredefinedInputMark : char list}
let Symbols = 
    {
        Inj = 
            {
                Start = ['{']
                End = ['}']
            }
        Sep = 
            {
                TokenAndInput = [':']
                IdentAndMember = ['.']
                InputAndInput = [',']
                
            }
        Escape = ['`']
        PredefinedInputMark = ['$']
    }
let IdentOptions = 
    IdentifierOptions 
        (
            isAsciiIdStart = (fun x -> isLetter x || x = '_'), 
            isAsciiIdContinue = (fun x -> isLetter x || isDigit x || x = '_')
        );
