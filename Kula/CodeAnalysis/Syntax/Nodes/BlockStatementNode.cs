using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The block statement node represents a block of statements.
/// </summary>
public sealed class BlockStatementNode : StatementNode
{
    public override SyntaxKind Kind => SyntaxKind.BlockStatement;
    
    /// <summary>
    /// The statements in the block.
    /// </summary>
    public List<StatementNode> Statements { get; }

    public BlockStatementNode(SourceLocation location, List<StatementNode> statements) : base(location)
    {
        Statements = statements;
    }
    
    public override IEnumerable<SyntaxNode> GetChildren() => Statements;
}