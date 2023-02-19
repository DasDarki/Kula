namespace Kula.CodeAnalysis.Syntax.Nodes;

public sealed class ValueNode : ExpressionNode
{
    public override SyntaxKind Kind => SyntaxKind.ValueExpression;
    
    /// <summary>
    /// The value of the node.
    /// </summary>
    public SyntaxToken Value { get; }
    
    public ValueNode(SyntaxToken value) : base(value.Location)
    {
        Value = value;
    }
}