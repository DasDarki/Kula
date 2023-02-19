using Kula.CodeAnalysis.Source;

namespace Kula.CodeAnalysis.Syntax.Nodes;

/// <summary>
/// The call expression node represents a call expression like "print(1)".
/// </summary>
public class CallExpressionNode : ExpressionNode
{
    public override SyntaxKind Kind => SyntaxKind.CallExpression;
    
    /// <summary>
    /// The class of the call expression. If the class is null, the function is not owned by a class.
    /// </summary>
    public SyntaxToken? Class { get; set; }
    
    /// <summary>
    /// The dot or colon token of the call expression. If the token is null, the function is not owned by a class.
    /// </summary>
    public SyntaxToken? Dot { get; set; }
    
    /// <summary>
    /// The name of the calling function.
    /// </summary>
    public SyntaxToken Name { get; set; }
    
    /// <summary>
    /// The opening parenthesis of the call expression.
    /// </summary>
    public SyntaxToken OpenParen { get; set; }
    
    /// <summary>
    /// The closing parenthesis of the call expression.
    /// </summary>
    public SyntaxToken CloseParen { get; set; }
    
    /// <summary>
    /// The arguments of the call expression.
    /// </summary>
    public List<ExpressionNode> Arguments { get; }

    public CallExpressionNode(SourceLocation location, SyntaxToken? @class, SyntaxToken? dot, SyntaxToken name, SyntaxToken openParen, SyntaxToken closeParen, List<ExpressionNode> arguments) : base(location)
    {
        Class = @class;
        Dot = dot;
        Name = name;
        OpenParen = openParen;
        CloseParen = closeParen;
        Arguments = arguments;
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        if (Class != null)
            yield return Class;
        
        if (Dot != null)
            yield return Dot;
        
        yield return Name;
        yield return OpenParen;
        
        foreach (var argument in Arguments)
            yield return argument;
        
        yield return CloseParen;
    }
}