using System.Collections;
using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The base class for all syntax nodes. Syntax nodes are the representation of the source code already transformed
/// into a syntax tree.
/// </summary>
public abstract class SyntaxNode : IEnumerable<SyntaxNode>
{
    /// <summary>
    /// The parent of this syntax node. If this is null, then this is the root node.
    /// </summary>
    public SyntaxNode? Parent { get; internal set; }
    
    /// <summary>
    /// The location of the syntax node in the source code.
    /// </summary>
    public SourceLocation Location { get; }
    
    /// <summary>
    /// The category of this syntax node.
    /// </summary>
    public abstract SyntaxNodeCategory Category { get; }
    
    /// <summary>
    /// The kind of this syntax node.
    /// </summary>
    public abstract SyntaxKind Kind { get; }

    protected SyntaxNode(SourceLocation location, SyntaxNode? parent = null)
    {
        Parent = parent;
        Location = location;
    }

    /// <summary>
    /// Returns the children of this syntax node.
    /// </summary>
    public virtual IEnumerable<SyntaxNode> GetChildren() => Enumerable.Empty<SyntaxNode>();

    public IEnumerator<SyntaxNode> GetEnumerator() => GetChildren().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}