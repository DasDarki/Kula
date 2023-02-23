using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The parameter node represents a parameter.
/// </summary>
public class ParameterNode : SyntaxNode
{
    public override SyntaxNodeCategory Category => SyntaxNodeCategory.Misc;
    
    public override SyntaxKind Kind => SyntaxKind.Parameter;
    
    /// <summary>
    /// The name of the parameter. If the parameter is a vararg, the name is "...".
    /// </summary>
    public SyntaxToken Name { get; set; }
    
    /// <summary>
    /// The type of the parameter. If the type is null, the parameter is of type "any".
    /// </summary>
    public SyntaxToken? Type { get; set; }
    
    /// <summary>
    /// The default value of the parameter. If the default value is null, the parameter has no default value.
    /// </summary>
    public ExpressionNode? Default { get; set; }
    
    /// <summary>
    /// Whether the parameter is a vararg. Uses <see cref="Name"/> to determine this.
    /// </summary>
    public bool IsVararg => Name.Text.StartsWith("...");
    
    public ParameterNode(SourceLocation location, SyntaxToken name, SyntaxToken? type, ExpressionNode? defaultValue) 
        : base(location)
    {
        Name = name;
        Type = type;
        Default = defaultValue;
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Name;
        if (Type != null) yield return Type;
        if (Default != null) yield return Default;
    }
}