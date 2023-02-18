using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The expression node is a base class for all expressions.
/// </summary>
public abstract class ExpressionNode : SyntaxNode
{
    public override SyntaxNodeCategory Category => SyntaxNodeCategory.Expression;

    protected ExpressionNode(SourceLocation location, SyntaxNode? parent = null) : base(location, parent)
    {
    }
}