using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis;

/// <summary>
/// The diagnostic sink is used to collect diagnostics.
/// </summary>
public sealed class DiagnosticSink
{
    /// <summary>
    /// The collected diagnostics.
    /// </summary>
    public IReadOnlyList<Diagnostic> Diagnostics => _diagnostics.AsReadOnly();
    
    private readonly List<Diagnostic> _diagnostics = new();

    /// <summary>
    /// Adds a diagnostic to the list of diagnostics.
    /// </summary>
    public void Report(Diagnostic diagnostic)
    {
        _diagnostics.Add(diagnostic);
    }
    
    /// <summary>
    /// Reports multiple diagnostics.
    /// </summary>
    public void Report(params Diagnostic[] diagnostics)
    {
        _diagnostics.AddRange(diagnostics);
    }
    
    /// <summary>
    /// Reports an error with the given location and message and a possible hint.
    /// </summary>
    public void ReportError(SourceLocation location, string message, string? hint = null)
    {
        Report(new Diagnostic(DiagnosticSeverity.Error, location, message, hint));
    }
    
    /// <summary>
    /// Reports a warning with the given location and message and a possible hint.
    /// </summary>
    public void ReportWarning(SourceLocation location, string message, string? hint = null)
    {
        Report(new Diagnostic(DiagnosticSeverity.Warning, location, message, hint));
    }
}