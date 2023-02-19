using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The expression statement node represents an expression statement.
/// </summary>
public sealed class ExpressionStatementNode : StatementNode
{
    public override SyntaxKind Kind => SyntaxKind.ExpressionStatement;
    
    /// <summary>
    /// The expression of the expression statement.
    /// </summary>
    public ExpressionNode Expression { get; set; }
    
    public ExpressionStatementNode(SourceLocation location, ExpressionNode expression) : base(location)
    {
        Expression = expression;
    }
    
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Expression;
    }
}