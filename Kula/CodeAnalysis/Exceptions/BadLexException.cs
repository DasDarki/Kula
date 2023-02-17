using Kula.CodeAnalysis.Syntax;

namespace Kula.CodeAnalysis.Exceptions;

internal sealed class BadLexException : Exception
{
    internal SpecificationResult Result { get; }
    
    internal BadLexException(SpecificationResult result, string? message = null) : base(message ?? $"Bad lex: {result.Text}")
    {
        Result = result;
    }
}