using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The script node is the root of the syntax tree and represents the entire script file.
/// </summary>
public sealed class ScriptNode : StatementNode 
{
    public override SyntaxKind Kind => SyntaxKind.ScriptStatement;

    /// <summary>
    /// The children of the script node.
    /// </summary>
    public List<SyntaxNode> Children { get; } = new();

    public ScriptNode(string? file) : base(BuildLocation(file))
    {
    }

    public override IEnumerable<SyntaxNode> GetChildren() => Children;

    private static SourceLocation BuildLocation(string? file)
    {
        return new SourceLocation(0, new SourceSpan(0, 0), file);
    }
}