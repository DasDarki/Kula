namespace Kula.CodeAnalysis.Syntax;

internal interface ITokenSpecifier
{
    bool IsMatch(LexerContext context);
    
    SpecificationResult Transform(LexerContext context);
}