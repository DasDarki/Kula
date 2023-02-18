using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The function declaration node represents a function declaration.
/// </summary>
public sealed class FunctionDeclarationNode : DeclarationNode
{
    public override SyntaxKind Kind => SyntaxKind.FunctionDeclaration;
    
    /// <summary>
    /// The name of the function. If empty, the function is anonymous.
    /// </summary>
    public SyntaxToken Name { get; set; }
    
    /// <summary>
    /// The return type of the function. If null, the function returns void.
    /// </summary>
    public SyntaxToken? ReturnType { get; set; }
    
    /// <summary>
    /// A list of parameters the function takes.
    /// </summary>
    public List<ParameterNode> Parameters { get; }

    /// <summary>
    /// Whether the function is anonymous. Uses <see cref="Name"/> to determine this.
    /// </summary>
    public bool IsAnonymous => string.IsNullOrEmpty(Name.Text);

    public FunctionDeclarationNode(SourceLocation location, SyntaxNode parent, SyntaxToken name, SyntaxToken? returnType) 
        : base(location, parent)
    {
        Name = name;
        ReturnType = returnType;
        Parameters = new List<ParameterNode>();
    }
}