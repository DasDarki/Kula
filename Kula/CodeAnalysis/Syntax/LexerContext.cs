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
    
    public char Peek(int offset, bool skipWhitespace = false)
    {
        var index = Position + offset;
        if (index >= Text.Length)
            return '\0';
        
        var c = Text[index];
        if (!skipWhitespace) 
            return c;
        
        while (char.IsWhiteSpace(c))
        {
            index++;
            if (index >= Text.Length)
                return '\0';
            c = Text[index];
        }

        return c;
    }
    
    public char PeekWithIndex(int offset, out int index, bool skipWhitespace = false)
    {
        index = Position + offset;
        if (index >= Text.Length)
            return '\0';
        
        var c = Text[index];
        if (!skipWhitespace) 
            return c;
        
        while (char.IsWhiteSpace(c))
        {
            index++;
            if (index >= Text.Length)
                return '\0';
            c = Text[index];
        }

        return c;
    }

    public void NextToken(bool skipWhitespace = false) => Jump(1, skipWhitespace);

    public void Jump(int offset, bool skipWhitespace = false)
    {
        Position += offset;
        SourcePosition += offset;
        
        if (!skipWhitespace) 
            return;

        while (char.IsWhiteSpace(Current))
        {
            Position++;
            SourcePosition++;
        }
    }
    
    public void NewLine()
    {
        SourcePosition = 0;
    }
    
    public char Read(bool skipWhitespace = false)
    {
        var current = Current;
        NextToken(skipWhitespace);
        return current;
    }
}