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
        
        var lexer = new Lexer(text, file);
        var tokens = lexer.Tokenize();

        foreach (var token in tokens.TakeWhile(token => token.Kind != SyntaxTokenKind.EndOfFileToken))
        {
            if (token.Kind == SyntaxTokenKind.BadToken)
            {
                Diagnostics.ReportError(token.Location, $"Unexpected token '{token.Text}'.");
                continue;
            }
            
            
        }
        
        ParsedScripts.Add(script);
    }
}