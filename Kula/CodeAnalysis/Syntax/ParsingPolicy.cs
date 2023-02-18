namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The parsing policy defines how the parser should behave in error situations.
/// </summary>
public enum ParsingPolicy
{
    /// <summary>
    /// The default policy. If an error is encountered, the parser will still finish the AST but won't add it to the list of parsed scripts.
    /// The errors will be added to the <see cref="DiagnosticSink"/> of the parser.
    /// </summary>
    SkipErroredAST,
    /// <summary>
    /// The parser will stop parsing the current script if an error is encountered.
    /// </summary>
    CancelParsingOnFirstError,
    /// <summary>
    /// The parser will ignore errors and continue parsing the script. This is useful for syntax highlighting.
    /// The AST will be added to the list of parsed scripts and the errors will be added to the <see cref="DiagnosticSink"/> of the parser.
    /// </summary>
    IgnoreErrors
}