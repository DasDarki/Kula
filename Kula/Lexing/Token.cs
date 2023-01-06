namespace Kula.Lexing;

/// <summary>
/// A token describes a single lexical unit in a source file.
/// </summary>
internal class Token
{
    /// <summary>
    /// The type of the token.
    /// </summary>
    public TokenType Type { get; }
    
    /// <summary>
    /// The value of token - if it has a value.
    /// </summary>
    public string? Value { get; set; }

    internal Token(TokenType type, string? value = null)
    {
        Type = type;
        Value = value;
    }
}