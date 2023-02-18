using Kula.CodeAnalysis.Syntax.Specifiers;

namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The kind of the syntax token.
/// </summary>
public enum SyntaxKind
{
    // --- Lexer --- \\
    
    /// <summary>
    /// The token is used for any not recognized token.
    /// </summary>
    [TokenSpecification]
    BadToken,
    
    [TokenSpecification]
    EndOfFileToken,
    
    // Literals
    
    [TokenSpecification(typeof(NumberTokenSpecifier))]
    NumberToken,
    
    [TokenSpecification(typeof(StringTokenSpecifier))]
    StringToken,
    
    // Keywords
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "function")]
    FunctionToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "end")]
    EndToken,
    
    // Misc

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
    
    [TokenSpecification(typeof(CharTokenSpecifier), ':')]
    ColonToken,
    
    // Identifiers
    
    [TokenSpecification(typeof(IdentifierTokenSpecifier))]
    IdentifierToken,
    
    
    
    // --- Parser --- \\
    
    // Statements
    ScriptStatement,
    CallStatement,
    
    // Declarations
    FunctionDeclaration,
    ParameterDeclaration,
}