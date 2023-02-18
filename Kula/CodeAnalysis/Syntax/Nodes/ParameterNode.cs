using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The parameter node represents a parameter.
/// </summary>
public class ParameterNode : DeclarationNode
{
    public override SyntaxNodeKind Kind => SyntaxNodeKind.Parameter;
    
    /// <summary>
    /// The name of the parameter. If the parameter is a vararg, the name is "...".
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The type of the parameter. If the type is not specified, the type is "any".
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// Whether the parameter is a vararg. Uses <see cref="Name"/> to determine this.
    /// </summary>
    public bool IsVararg => Name == "...";
    
    public ParameterNode(SourceLocation location, FunctionDeclarationNode function, string name, string type) 
        : base(location, function)
    {
        Name = name;
        Type = type;
    }
}