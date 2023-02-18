using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis;

/// <summary>
/// The diagnostics class is used to report errors and warnings.
/// </summary>
public sealed class Diagnostic
{
    /// <summary>
    /// The severity of the diagnostic.
    /// </summary>
    public DiagnosticSeverity Severity { get; }
    
    /// <summary>
    /// The location of the diagnostic.
    /// </summary>
    public SourceLocation Location { get; }
    
    /// <summary>
    /// The message of the diagnostic.
    /// </summary>
    public string Message { get; }
    
    /// <summary>
    /// A hint for the diagnostic. If the diagnostic is an error, this is the suggested fix.
    /// </summary>
    public string? Hint { get; }
    
    public Diagnostic(DiagnosticSeverity severity, SourceLocation location, string message, string? hint = null)
    {
        Severity = severity;
        Location = location;
        Message = message;
        Hint = hint;
    }
}