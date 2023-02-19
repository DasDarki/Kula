using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The statement node is a base class for all statements.
/// </summary>
public abstract class StatementNode : SyntaxNode
{
    public override SyntaxNodeCategory Category => SyntaxNodeCategory.Statement;

    protected StatementNode(SourceLocation location) : base(location)
    {
    }
}