using System.Text;
using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The syntax token is a token in the syntax tree.
/// </summary>
public sealed class SyntaxToken
{
    /// <summary>
    /// The location of the token in the original source code.
    /// </summary>
    public SourceLocation Location { get; }
    
    /// <summary>
    /// The type of the syntax token.
    /// </summary>
    public SyntaxTokenKind Kind { get; }
    
    /// <summary>
    /// The raw text of the token.
    /// </summary>
    public string Text { get; }
    
    /// <summary>
    /// The value of the token.
    /// </summary>
    public object? Value { get; }

    internal SyntaxToken(SourceLocation location, SyntaxTokenKind kind, string text, object? value = null)
    {
        Location = location;
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

        if (Kind != SyntaxTokenKind.EndOfFileToken)
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