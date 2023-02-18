namespace Kula.CodeAnalysis.Source;

/// <summary>
/// The source span is a range of characters in the source text.
/// </summary>
public readonly struct SourceSpan
{
    public int Start { get; }
    
    public int Length { get; }
    
    public int End => Start + Length;
    
    internal SourceSpan(int start, int length)
    {
        Start = start;
        Length = length;
    }
    
    public override string ToString() => $"{Start}..{End}";
    
    public bool Contains(SourceSpan other) => Start <= other.Start && End >= other.End;
    
    public bool Overlaps(SourceSpan other) => Start <= other.End && End >= other.Start;
    
    public static SourceSpan operator +(SourceSpan left, SourceSpan right) => FromBounds(left.Start, right.End);
    
    internal static SourceSpan FromBounds(int start, int end) => new(start, end - start);
}