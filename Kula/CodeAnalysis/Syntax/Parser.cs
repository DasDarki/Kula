using Kula.CodeAnalysis.Syntax.Nodes;

namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The parser is responsible for taking a sequence of tokens and turning them into a syntax tree.
/// </summary>
public sealed class Parser
{
    /// <summary>
    /// The parsed scripts in AST representation.
    /// </summary>
    public List<ScriptNode> ParsedScripts { get; } = new();

    /// <summary>
    /// The diagnostics the parser has encountered.
    /// </summary>
    public DiagnosticSink Diagnostics { get; } = new();
    
    /// <summary>
    /// The parsing policy of this parser.
    /// </summary>
    public ParsingPolicy Policy { get; set; }

    /// <summary>
    /// Constructs a new parser.
    /// </summary>
    public Parser(ParsingPolicy policy = ParsingPolicy.SkipErroredAST)
    {
        Policy = policy;
    }

    /// <summary>
    /// Parses the given file and adds the parsed script to the <see cref="ParsedScripts"/> list.
    /// </summary>
    /// <param name="path">The path to the script.</param>
    public void ParseFile(string path) => Parse(File.ReadAllText(path), path);
    
    /// <summary>
    /// Parses the given text and adds the parsed script to the <see cref="ParsedScripts"/> list.
    /// </summary>
    public void Parse(string text, string? file = null)
    {
        var script = new ScriptNode(file);
        var currentErrorCount = Diagnostics.ErrorCount;
        
        var lexer = new Lexer(text, file);
        var context = new ParserContext(lexer);

        while (context.HasNext)
        {
            var token = context.Read();
            
            if (token.Kind == SyntaxTokenKind.BadToken)
            {
                Diagnostics.ReportError(token.Location, $"Unexpected token '{token.Text}'.");

                if (Policy == ParsingPolicy.CancelParsingOnFirstError)
                {
                    return;
                }
                
                continue;
            }
            
            if (token.Kind == SyntaxTokenKind.EndOfFileToken)
            {
                break;
            }

            switch (token.Kind)
            {
                case SyntaxTokenKind.FunctionToken:
                    ParseFunction(script, context);
                    break;
            }
        }
        
        if (Policy == ParsingPolicy.IgnoreErrors || (Policy == ParsingPolicy.SkipErroredAST && Diagnostics.ErrorCount == currentErrorCount))
        {
            ParsedScripts.Add(script);
        }
    }

    private void ParseFunction(ScriptNode script, ParserContext context)
    {
        
    }
}