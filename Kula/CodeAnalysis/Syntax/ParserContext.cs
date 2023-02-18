namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The parser context wraps the lexers output and provides a way to read the tokens.
/// </summary>
internal sealed class ParserContext
{
    private readonly List<SyntaxToken> _tokens;
    private int _position;

    internal SyntaxToken Current => Peek();
    
    internal SyntaxToken Next => Peek(1);
    
    internal bool HasNext => _position < _tokens.Count;

    internal ParserContext(Lexer lexer)
    {
        _tokens = lexer.Tokenize();
    }

    internal SyntaxToken Peek(int offset = 0)
    {
        return _position + offset >= _tokens.Count ? _tokens[^1] : _tokens[_position + offset];
    }

    internal void NextToken() => Jump(1);
    
    internal void Jump(int offset)
    {
        _position += offset;
    }

    internal SyntaxToken Read()
    {
        var token = Current;
        NextToken();
        return token;
    }
}