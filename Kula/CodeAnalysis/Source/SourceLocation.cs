namespace Kula.CodeAnalysis.Source;

/// <summary>
/// The source location defines the location of a token in the source code.
/// </summary>
public sealed class SourceLocation
{
    /// <summary>
    /// The line of the location.
    /// </summary>
    public int Line { get; }
    
    /// <summary>
    /// The source span declaring the column of the location.
    /// </summary>
    public SourceSpan Column { get; }
    
    /// <summary>
    /// The file name from which the tokens location is. If the token is not from a file, this is null.
    /// </summary>
    public string? File { get; }
    
    internal SourceLocation(int line, SourceSpan column, string? file = null)
    {
        Line = line;
        Column = column;
        File = file;
    }
}