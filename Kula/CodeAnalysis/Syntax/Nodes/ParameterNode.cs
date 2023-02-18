using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The parameter node represents a parameter.
/// </summary>
public class ParameterNode : DeclarationNode
{
    public override SyntaxKind Kind => SyntaxKind.ParameterDeclaration;
    
    /// <summary>
    /// The name of the parameter. If the parameter is a vararg, the name is "...".
    /// </summary>
    public SyntaxToken Name { get; set; }
    
    /// <summary>
    /// The type of the parameter. If the type is null, the parameter is of type "any".
    /// </summary>
    public SyntaxToken? Type { get; set; }
    
    /// <summary>
    /// Whether the parameter is a vararg. Uses <see cref="Name"/> to determine this.
    /// </summary>
    public bool IsVararg => Name.Text == "...";
    
    public ParameterNode(SourceLocation location, FunctionDeclarationNode function, SyntaxToken name, SyntaxToken? type) 
        : base(location, function)
    {
        Name = name;
        Type = type;
    }
}