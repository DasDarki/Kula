using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax;

internal readonly struct SpecificationResult
{
    public string Text { get; }

    public SourceSpan Span { get; }
    
    public object? Value { get; }

    internal SpecificationResult(string text, SourceSpan span, object? value = null)
    {
        Text = text;
        Span = span;
        Value = value;
    }

    public void Deconstruct(out string text, out SourceSpan span, out object? value)
    {
        text = Text;
        span = Span;
        value = Value;
    }
}