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
    
    [TokenSpecification(typeof(BooleanTokenSpecifier))]
    BooleanToken,
    
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
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "...")]
    ThreeDotsToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '.')]
    DotToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), ',')]
    CommaToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '(')]
    OpenParenthesisToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), ')')]
    CloseParenthesisToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), ':')]
    ColonToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '=')]
    EqualsToken,
    
    // Identifiers
    
    [TokenSpecification(typeof(IdentifierTokenSpecifier))]
    IdentifierToken,
    
    
    
    // --- Parser --- \\
    
    // Statements
    ScriptStatement,
    BlockStatement,
    ExpressionStatement,
    
    // Declarations
    FunctionDeclaration,
    ParameterDeclaration,
    
    // Expressions
    ValueExpression,
    CallExpression,
}