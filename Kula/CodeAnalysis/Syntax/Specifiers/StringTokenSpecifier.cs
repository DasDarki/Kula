using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Specifiers;

internal sealed class StringTokenSpecifier : ITokenSpecifier
{
    private readonly string _pattern;
    
    public StringTokenSpecifier(string pattern)
    {
        _pattern = pattern;
    }
    
    public bool IsMatch(LexerContext context) => !_pattern.Where((t, i) => context.Peek(i) != t).Any();

    public SpecificationResult Transform(LexerContext context)
    {
        var span = new SourceSpan(context.SourcePosition, _pattern.Length);
        context.Jump(_pattern.Length);
        
        return new SpecificationResult(_pattern, span);
    }
}