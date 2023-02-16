using Kula.CodeAnalysis.Syntax.Specifiers;

namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The kind of the syntax token.
/// </summary>
public enum SyntaxTokenKind
{
    /// <summary>
    /// The token is used for any not recognized token.
    /// </summary>
    [TokenSpecification]
    BadToken,
    
    [TokenSpecification]
    EndOfFileToken,
    
    // Misc
    
    [TokenSpecification(typeof(NumberTokenSpecifier))]
    NumberToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '+')]
    PlusToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '-')]
    MinusToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '*')]
    StarToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '/')]
    SlashToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '%')]
    PercentToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '^')]
    CaretToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '.')]
    DotToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '(')]
    OpenParenthesisToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), ')')]
    CloseParenthesisToken,
}