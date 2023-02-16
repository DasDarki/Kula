using System.Globalization;
using System.Text;
using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Specifiers;

internal sealed class NumberTokenSpecifier : ITokenSpecifier
{
    public bool IsMatch(LexerContext context) =>
        char.IsDigit(context.Current)
        || context.Current == '-' && char.IsDigit(context.Peek(1))
        || context.Current == '+' && char.IsDigit(context.Peek(1))
        || context.Current == '.' && char.IsDigit(context.Peek(1))
        || context.Current == '-' && context.Peek(1) == '.' && char.IsDigit(context.Peek(2))
        || context.Current == '+' && context.Peek(1) == '.' && char.IsDigit(context.Peek(2));

    public SpecificationResult Transform(LexerContext context)
    {
        var start = context.SourcePosition;
        var text = ReadNumber(context);
        
        return new SpecificationResult(text, new SourceSpan(start, text.Length), double.Parse(text, CultureInfo.InvariantCulture));
    }
    
    private string ReadNumber(LexerContext context)
    {
        var sb = new StringBuilder();
        
        if (context.Current is '-' or '+')
            sb.Append(context.Read());
        
        while (char.IsDigit(context.Current))
            sb.Append(context.Read());
        
        if (context.Current == '.')
        {
            sb.Append(context.Read());
        }
        
        while (char.IsDigit(context.Current))
            sb.Append(context.Read());

        if (context.Current is not 'e') return sb.ToString();
        
        sb.Append(context.Read());
            
        if (context.Current is '-' or '+')
            sb.Append(context.Read());
            
        while (char.IsDigit(context.Current))
            sb.Append(context.Read());

        return sb.ToString();
    }
}