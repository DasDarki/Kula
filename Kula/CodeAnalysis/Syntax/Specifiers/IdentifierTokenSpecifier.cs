using System.Text;
using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Specifiers;

internal sealed class IdentifierTokenSpecifier : ITokenSpecifier
{
    public bool IsMatch(LexerContext context) => IsIdentifierStart(context.Current);

    public SpecificationResult Transform(LexerContext context)
    {
        var start = context.SourcePosition;
        var text = ReadIdentifier(context);
        return new SpecificationResult(text, new SourceSpan(start, text.Length), text);
    }
    
    internal static bool IsIdentifierStart(char c)
    {
        return char.IsLetter(c) || c == '_';
    }
    
    internal static string ReadIdentifier(LexerContext context)
    {
        var sb = new StringBuilder();
        sb.Append(context.Read());
        
        while (char.IsLetterOrDigit(context.Current) || context.Current == '_')
            sb.Append(context.Read());
        
        return sb.ToString();
    }
}