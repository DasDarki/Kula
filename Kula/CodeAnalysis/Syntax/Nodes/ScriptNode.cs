using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The script node is the root of the syntax tree and represents the entire script file.
/// </summary>
public sealed class ScriptNode : SyntaxNode
{
    public override SyntaxNodeCategory Category => SyntaxNodeCategory.Misc;
    
    public override SyntaxKind Kind => SyntaxKind.Script;

    /// <summary>
    /// The members of the script node.
    /// </summary>
    public List<SyntaxNode> Members { get; }
    
    /// <summary>
    /// The end of file token ending the script.
    /// </summary>
    public SyntaxToken EndOfFileToken { get; }

    public ScriptNode(string? file, List<SyntaxNode> members, SyntaxToken endOfFileToken) : base(BuildLocation(file))
    {
        Members = members;
        EndOfFileToken = endOfFileToken;
    }

    public override IEnumerable<SyntaxNode> GetChildren() => Members;

    private static SourceLocation BuildLocation(string? file)
    {
        return new SourceLocation(0, new SourceSpan(0, 0), file);
    }
}