using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Specifiers;

internal sealed class BooleanTokenSpecifier : ITokenSpecifier
{
    public bool IsMatch(LexerContext context)
    {
        var c = context.Current;
        return c switch
        {
            'f' => context.Peek(1) == 'a' && context.Peek(2) == 'l' && context.Peek(3) == 's' && context.Peek(4) == 'e',
            't' => context.Peek(1) == 'r' && context.Peek(2) == 'u' && context.Peek(3) == 'e',
            _ => false
        };
    }

    public SpecificationResult Transform(LexerContext context)
    {
        var start = context.SourcePosition;
        
        var c = context.Current;
        var text = c switch
        {
            'f' => context.Read(5),
            't' => context.Read(4),
            _ => throw new InvalidOperationException()
        };
        
        return new SpecificationResult(text, new SourceSpan(start, text.Length), c == 't');
    }
}