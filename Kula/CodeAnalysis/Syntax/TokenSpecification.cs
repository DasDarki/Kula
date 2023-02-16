using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The token specification attribute is used to mark the enum values of the syntax token kind enum.
/// It holds information on how to lex the token.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
internal class TokenSpecification : Attribute
{
    private readonly ITokenSpecifier? _specifier;

    internal TokenSpecification(Type type, object? args)
    {
        _specifier = (ITokenSpecifier) Activator.CreateInstance(type, args)!;
    }

    internal TokenSpecification(Type type)
    {
        _specifier = (ITokenSpecifier) Activator.CreateInstance(type)!;
    }
    
    internal TokenSpecification()
    {
    }

    internal bool IsMatch(LexerContext context) => _specifier?.IsMatch(context) ?? false;

    internal SpecificationResult Transform(LexerContext context)
    {
        if (_specifier != null)
            return _specifier.Transform(context);
        
        var span = new SourceSpan(context.SourcePosition, 1);
        context.NextToken();

        return new SpecificationResult(string.Empty, span);
    }
}