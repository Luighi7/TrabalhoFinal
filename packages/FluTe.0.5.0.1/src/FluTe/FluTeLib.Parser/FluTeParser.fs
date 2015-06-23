module public FluTeLib.Parser.FluTeParser
open FluTeLib.Core.Template

///<summary>Parses a template string into a FluTeTemplate object.</summary>
let public Parse str = FluTeCorePrototype(ParserEngine.Parse str)