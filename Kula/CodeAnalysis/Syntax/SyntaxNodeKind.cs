namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The kind of the syntax node.
/// </summary>
public enum SyntaxNodeKind
{
    // Statements
    Script,
    CallStatement,
    
    // Declarations
    FunctionDeclaration,
    Parameter,
}