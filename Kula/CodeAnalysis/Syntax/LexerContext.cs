namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The lexer context represents the context of the lexer. It allows the lexer to peek at the next character.
/// </summary>
internal sealed class LexerContext
{
    public string Text { get; }
    
    public int Position { get; private set; }
    
    public int SourcePosition { get; set; }
    
    public char Current => Peek(0);
    
    public char Next => Peek(1);

    internal LexerContext(string text)
    {
        Text = text;
    }
    
    public char Peek(int offset)
    {
        var index = Position + offset;
        return index >= Text.Length ? '\0' : Text[index];
    }

    public void NextToken()
    {
        Position++;
        SourcePosition++;
    }

    public void Jump(int offset)
    {
        Position += offset;
        SourcePosition += offset;
    }
    
    public void NewLine()
    {
        SourcePosition = 0;
    }
    
    public char Read()
    {
        var current = Current;
        NextToken();
        return current;
    }
}