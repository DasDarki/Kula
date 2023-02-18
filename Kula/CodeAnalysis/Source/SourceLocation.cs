using System.Text;

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
    public SourceSpan Column { get; private set; }
    
    /// <summary>
    /// The file name from which the tokens location is. If the token is not from a file, this is null.
    /// </summary>
    public string? File { get; }
    
    /// <summary>
    /// Overrides the addition operator to add two source locations.
    /// </summary>
    public static SourceLocation operator +(SourceLocation left, SourceLocation right) => left.Add(right);
    
    internal SourceLocation(int line, SourceSpan column, string? file = null)
    {
        Line = line;
        Column = column;
        File = file;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append('(');

        if (File != null)
        {
            builder.Append(File);
            builder.Append(' ');
            builder.Append('@');
            builder.Append(' ');
        }

        builder.Append(Line);
        builder.Append(':');
        builder.Append(Column);
        builder.Append(')');
        
        return builder.ToString();
    }

    public SourceLocation Add(SourceLocation other)
    {
        if (other.File != File)
            throw new ArgumentException("Cannot add two locations from different files.", nameof(other));

        if (other.Line != Line)
            throw new ArgumentException("Cannot add two locations from different lines.", nameof(other));
        
        Column += other.Column;
        return this;
    }
}