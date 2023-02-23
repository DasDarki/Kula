using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The function declaration node represents a function declaration.
/// </summary>
public sealed class FunctionDeclarationNode : DeclarationNode
{
    public override SyntaxKind Kind => SyntaxKind.FunctionDeclaration;
    
    /// <summary>
    /// The local keyword of the function. If null, the function is not local.
    /// </summary>
    public SyntaxToken? Local { get; set; }
    
    /// <summary>
    /// The table of the function. If null, the function is not assigned to a table.
    /// </summary>
    public SyntaxToken? Table { get; set; }
    
    /// <summary>
    /// The name of the function. If empty, the function is anonymous.
    /// </summary>
    public SyntaxToken Name { get; set; }
    
    /// <summary>
    /// The return type of the function. If null, the function returns void.
    /// </summary>
    public SyntaxToken? ReturnType { get; set; }
    
    /// <summary>
    /// The opening parenthesis of the function declaration.
    /// </summary>
    public SyntaxToken OpenParen { get; }
    
    /// <summary>
    /// The closing parenthesis of the function declaration.
    /// </summary>
    public SyntaxToken CloseParen { get; }
    
    /// <summary>
    /// A list of parameters the function takes.
    /// </summary>
    public List<ParameterNode> Parameters { get; }
    
    /// <summary>
    /// The body of the function.
    /// </summary>
    public BlockStatementNode Body { get; set; }

    /// <summary>
    /// Whether the function is anonymous. Uses <see cref="Name"/> to determine this.
    /// </summary>
    public bool IsAnonymous => string.IsNullOrEmpty(Name.Text);
    
    /// <summary>
    /// Whether the function is local. Uses <see cref="Local"/> to determine this.
    /// </summary>
    public bool IsLocal => Local != null;

    public FunctionDeclarationNode(
        SourceLocation location, 
        SyntaxToken? local,
        SyntaxToken? table,
        SyntaxToken name,
        SyntaxToken? returnType, 
        SyntaxToken openParen, 
        SyntaxToken closeParen, 
        List<ParameterNode> parameters,
        BlockStatementNode body) : base(location)
    {
        Local = local;
        Table = table;
        Name = name;
        ReturnType = returnType;
        OpenParen = openParen;
        CloseParen = closeParen;
        Parameters = parameters;
        Body = body;
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        if (Local != null)
            yield return Local;
        
        yield return Name;
        yield return OpenParen;
        foreach (var parameter in Parameters)
            yield return parameter;
        yield return CloseParen;
        
        if (ReturnType != null)
            yield return ReturnType;
        
        yield return Body;
    }
}