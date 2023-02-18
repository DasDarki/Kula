using System.Text;
using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The syntax token is a token in the syntax tree.
/// </summary>
public sealed class SyntaxToken : SyntaxNode
{
    public override SyntaxNodeCategory Category => SyntaxNodeCategory.Token;
    
    public override SyntaxKind Kind { get; }
    
    /// <summary>
    /// The raw text of the token.
    /// </summary>
    public string Text { get; }
    
    /// <summary>
    /// The value of the token.
    /// </summary>
    public object? Value { get; }

    public SyntaxToken(SourceLocation location, SyntaxKind kind, string text, object? value = null) : base(location)
    {
        Kind = kind;
        Text = text;
        Value = value;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append(Location);
        builder.Append(' ');
        builder.Append(Enum.GetName(Kind));

        if (Kind != SyntaxKind.EndOfFileToken)
        {
            builder.Append(':');
            builder.Append(' ');
        }
        
        if (Value != null)
        {
            builder.Append(Value);
            builder.Append(' ');
            builder.Append('[');
            builder.Append(Value.GetType().Name);
            builder.Append(']');
        }
        else
        {
            builder.Append(Text);
        }
        
        return builder.ToString();
    }
}