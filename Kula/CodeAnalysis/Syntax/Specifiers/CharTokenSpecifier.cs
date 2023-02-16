using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Specifiers;

internal sealed class CharTokenSpecifier : ITokenSpecifier
{
    private readonly char _c;
    
    public CharTokenSpecifier(char c)
    {
        _c = c;
    }

    public bool IsMatch(LexerContext context) => _c == context.Current;

    public SpecificationResult Transform(LexerContext context) => new(context.Read().ToString(), new SourceSpan(context.SourcePosition - 1, 1));
}