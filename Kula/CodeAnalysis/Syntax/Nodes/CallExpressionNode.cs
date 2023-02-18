using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The call expression node represents a call expression like "print(1)".
/// </summary>
public class CallExpressionNode : StatementNode
{
    public override SyntaxKind Kind => SyntaxKind.CallStatement;
    
    public CallExpressionNode(SourceLocation location, SyntaxNode parent) 
        : base(location, parent)
    {
    }
}