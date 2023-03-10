using System.Reflection;
using Kula.CodeAnalysis.Exceptions;
using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The lexer is used to split the source code into tokens.
/// </summary>
internal sealed class Lexer
{
    private readonly Dictionary<SyntaxKind, TokenSpecification> _tokenSpecifications = new();
    private readonly LexerContext _context;
    private readonly string? _file;
    private int _line = 1;
    
    internal Lexer(string text, string? file = null)
    {
        _file = file;
        _context = new LexerContext(text);
        
        foreach (var value in Enum.GetValues(typeof(SyntaxKind)))
        {
            if (value is not SyntaxKind kind)
                continue;
            
            var attribute = typeof(SyntaxKind).GetField(kind.ToString())?.GetCustomAttribute<TokenSpecification>();
            
            if (attribute == null)
                continue;
            
            _tokenSpecifications.Add(kind, attribute);
        }
    }
    
    internal List<SyntaxToken> Tokenize()
    {
        var tokens = new List<SyntaxToken>();
        
        while (_context.Current != '\0')
        {
            switch (_context.Current)
            {
                case '\r' or '\t':
                    _context.NextToken();
                    continue;
                case '\n':
                    _context.NewLine();
                    _context.NextToken();
                    _line++;
                    continue;
            }
            
            var found = false;
            
            foreach (var (kind, specification) in _tokenSpecifications)
            {
                if (!specification.IsMatch(_context))
                    continue;

                try
                {
                    var (text, span, value) = specification.Transform(_context);
                    tokens.Add(new SyntaxToken(GetLocation(span), kind, text, value));
                }
                catch (BadLexException ex)
                {
                    var (text, span, value) = ex.Result;
                    tokens.Add(new SyntaxToken(GetLocation(span), SyntaxKind.BadToken, text, value));
                }
                
                found = true;
                break;
            }
            
            if (found)
                continue;

            if (char.IsWhiteSpace(_context.Current))
            {
                _context.NextToken();
                continue;
            }
            
            tokens.Add(new SyntaxToken(GetLocation(new SourceSpan(_context.SourcePosition, 1)), SyntaxKind.BadToken, _context.Read().ToString()));
        }
        
        tokens.Add(new SyntaxToken(GetLocation(new SourceSpan(_context.SourcePosition, 0)), SyntaxKind.EndOfFileToken, string.Empty));
        
        return tokens;
    }

    private SourceLocation GetLocation(SourceSpan column) => new(_line, column, _file);
}