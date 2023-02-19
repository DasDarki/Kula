namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The parser context wraps the lexers output and provides a way to read the tokens.
/// </summary>
internal sealed class ParserContext
{
    private readonly DiagnosticSink _diagnostics;
    private readonly List<SyntaxToken> _tokens;
    private int _position;

    internal SyntaxToken Current => Peek();
    
    internal SyntaxToken Next => Peek(1);
    
    internal bool HasNext => _position < _tokens.Count || Next.Kind != SyntaxKind.EndOfFileToken;

    internal ParserContext(Lexer lexer, DiagnosticSink diagnostics)
    {
        _tokens = lexer.Tokenize();
        _diagnostics = diagnostics;
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
    
    internal SyntaxToken Match(SyntaxKind kind, string? hint = null)
    {
        if (Current.Kind == kind)
        {
            return Read();
        }

        _diagnostics.ReportError(Current.Location, $"Unexpected token '{Current.Text}'. Expected '{Enum.GetName(kind)}'.", hint);
        return new SyntaxToken(Current.Location, kind, string.Empty);
    }
}