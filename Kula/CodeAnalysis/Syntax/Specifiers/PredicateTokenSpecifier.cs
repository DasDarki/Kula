using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Specifiers;

internal class PredicateTokenSpecifier : ITokenSpecifier
{
    private readonly Predicate<char> _predicate;
    
    public PredicateTokenSpecifier(Predicate<char> predicate)
    {
        _predicate = predicate;
    }

    public bool IsMatch(LexerContext context) => _predicate(context.Current);

    public SpecificationResult Transform(LexerContext context) => new(context.Read().ToString(), new SourceSpan(context.SourcePosition - 1, 1));
}