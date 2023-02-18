using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The declaration node is a base class for all declarations.
/// </summary>
public abstract class DeclarationNode : SyntaxNode
{
    public override SyntaxNodeCategory Category => SyntaxNodeCategory.Declaration;

    protected DeclarationNode(SourceLocation location, SyntaxNode? parent = null) : base(location, parent)
    {
    }
}