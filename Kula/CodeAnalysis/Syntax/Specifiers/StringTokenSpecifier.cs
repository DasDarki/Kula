using System.Text;
using Kula.CodeAnalysis.Exceptions;
using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Specifiers;

internal sealed class StringTokenSpecifier : ITokenSpecifier
{
    public bool IsMatch(LexerContext context) => context.Current is '"' or '\'';

    public SpecificationResult Transform(LexerContext context)
    {
        var start = context.SourcePosition;
        var quote = context.Read();
        var text = ReadString(context, quote, out var errored);

        var result = new SpecificationResult(text, new SourceSpan(start, text.Length), text);

        if (errored)
            throw new BadLexException(result, "String must end with a " + quote);

        return result;
    }

    private string ReadString(LexerContext context, char quote, out bool errored)
    {
        var sb = new StringBuilder();

        var wasEscaped = false;
        errored = false;

        while (true)
        {
            var current = context.Current;

            if (current == quote && !wasEscaped)
                break;

            if (current == '\0')
            {
                errored = true;
                break;
            }
            
            if (current == '\\' && !wasEscaped)
            {
                wasEscaped = true;
                context.Read();
                continue;
            }
            
            wasEscaped = false;
            sb.Append(context.Read());
        }

        context.Read();
        
        return sb.ToString();
    }
}