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
        sb.Append(quote);

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

            if (current == '$' && context.Peek(1) == '{')
            {
                context.Jump(2);
                var identifier = ReadTemplatingValue(context, out errored);

                if (errored)
                {
                    break;
                }
                
                sb.Append(quote);
                sb.Append(" .. ");
                sb.Append($"tostring({identifier})");
                sb.Append(" .. ");
                sb.Append(quote);
                continue;
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
        
        sb.Append(context.Read());
        
        return sb.ToString();
    }
    
    private string ReadTemplatingValue(LexerContext context, out bool errored)
    {
        var sb = new StringBuilder();
        var identifierRead = false;
        errored = false;

        while (true)
        {
            var current = context.Current;

            if (current is ' ' or '\t' or '\r')
            {
                context.Read();
                continue;
            }

            if (current == '}')
            {
                context.Read();
                break;
            }

            if (current is '\0' or '\n')
            {
                errored = true;
                break;
            }

            if (!IdentifierTokenSpecifier.IsIdentifierStart(current) || identifierRead)
            {
                errored = true;
                break;
            }
            
            identifierRead = true;
            sb.Append(IdentifierTokenSpecifier.ReadIdentifier(context));
        }

        return sb.ToString();
    }
}