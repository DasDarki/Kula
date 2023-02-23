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
    
    // Keywords - FROM LUA
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "function")]
    FunctionToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "end")]
    EndToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "local")]
    LocalToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "if")]
    IfToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "then")]
    ThenToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "else")]
    ElseToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "elseif")]
    ElseIfToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "while")]
    WhileToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "do")]
    DoToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "repeat")]
    RepeatToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "until")]
    UntilToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "for")]
    ForToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "in")]
    InToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "return")]
    ReturnToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "break")]
    BreakToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "nil")]
    NilToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "and")]
    AndToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "or")]
    OrToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "not")]
    NotToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "self")]
    SelfToken,

    // Keywords - FROM KULA
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "class")]
    ClassToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "extends")]
    ExtendsToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "parent")]
    ParentToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "interface")]
    InterfaceToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "implements")]
    ImplementsToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "override")]
    OverrideToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "abstract")]
    AbstractToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "readonly")]
    ReadonlyToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "enum")]
    EnumToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "annotation")]
    AnnotationToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "is")]
    IsToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "as")]
    AsToken,
    
    [TokenSpecification(typeof(PatternTokenSpecifier), "instanceof")]
    InstanceofToken,
    
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
    
    [TokenSpecification(typeof(CharTokenSpecifier), '<')]
    LessThanToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '>')]
    GreaterThanToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '!')]
    ExclamationMarkToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '?')]
    QuestionMarkToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '~')]
    TildeToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '{')]
    OpenBraceToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '}')]
    CloseBraceToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), '[')]
    OpenBracketToken,
    
    [TokenSpecification(typeof(CharTokenSpecifier), ']')]
    CloseBracketToken,
    
    // Identifiers
    
    [TokenSpecification(typeof(IdentifierTokenSpecifier))]
    IdentifierToken,
    
    
    
    // --- Parser --- \\
    // Misc
    Script,
    Parameter,
    
    // Statements
    BlockStatement,
    ExpressionStatement,
    
    // Declarations
    FunctionDeclaration,
    GlobalStatementDeclaration,
    
    // Expressions
    ValueExpression,
    CallExpression,
}