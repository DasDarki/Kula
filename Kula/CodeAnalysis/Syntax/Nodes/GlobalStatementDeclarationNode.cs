using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// A wrapper for <see cref="StatementNode"/>s in the global scope.
/// </summary>
public class GlobalStatementDeclarationNode : DeclarationNode
{
    public override SyntaxKind Kind => SyntaxKind.GlobalStatementDeclaration;
    
    /// <summary>
    /// The statement for the global statement declaration.
    /// </summary>
    public StatementNode Statement { get; }
    
    public GlobalStatementDeclarationNode(StatementNode statement) : base(statement.Location)
    {
        Statement = statement;
    }
}