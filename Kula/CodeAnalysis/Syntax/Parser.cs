using Kula.CodeAnalysis.Source;
using Kula.CodeAnalysis.Syntax.Nodes;

namespace Kula.CodeAnalysis.Syntax;

/// <summary>
/// The parser is responsible for taking a sequence of tokens and turning them into a syntax tree.
/// </summary>
public sealed class Parser
{
    /// <summary>
    /// The parsed scripts in AST representation.
    /// </summary>
    public List<ScriptNode> ParsedScripts { get; } = new();

    /// <summary>
    /// The diagnostics the parser has encountered.
    /// </summary>
    public DiagnosticSink Diagnostics { get; } = new();
    
    /// <summary>
    /// The parsing policy of this parser.
    /// </summary>
    public ParsingPolicy Policy { get; set; }

    /// <summary>
    /// Constructs a new parser.
    /// </summary>
    public Parser(ParsingPolicy policy = ParsingPolicy.SkipErroredAST)
    {
        Policy = policy;
    }

    /// <summary>
    /// Parses the given file and adds the parsed script to the <see cref="ParsedScripts"/> list.
    /// </summary>
    /// <param name="path">The path to the script.</param>
    public void ParseFile(string path) => Parse(File.ReadAllText(path), path);
    
    /// <summary>
    /// Parses the given text and adds the parsed script to the <see cref="ParsedScripts"/> list.
    /// </summary>
    public void Parse(string text, string? file = null)
    {
        var script = new ScriptNode(file);
        var currentErrorCount = Diagnostics.ErrorCount;
        
        var lexer = new Lexer(text, file);
        var context = new ParserContext(lexer, Diagnostics);

        while (context.HasNext)
        {
            var token = context.Current;
            
            if (token.Kind == SyntaxKind.BadToken)
            {
                Diagnostics.ReportError(token.Location, $"Unexpected token '{token.Text}'.");

                if (Policy == ParsingPolicy.CancelParsingOnFirstError)
                {
                    return;
                }
                
                continue;
            }
            
            if (token.Kind == SyntaxKind.EndOfFileToken)
            {
                break;
            }

            switch (token.Kind)
            {
                case SyntaxKind.FunctionToken:
                    script.Children.Add(ParseFunction(context));
                    break;
                default:
                    script.Children.Add(ParseStatement(context));
                    break;
            }
            
            if (Policy == ParsingPolicy.CancelParsingOnFirstError && Diagnostics.ErrorCount > currentErrorCount) // check for a second time in case the error count has changed
            {
                return;
            }
        }
        
        if (Policy == ParsingPolicy.IgnoreErrors || (Policy == ParsingPolicy.SkipErroredAST && Diagnostics.ErrorCount == currentErrorCount))
        {
            ParsedScripts.Add(script);
        }
    }

    private BlockStatementNode ParseBlock(ParserContext context)
    {
        var start = context.Current.Location;
        var statements = new List<StatementNode>();
        
        while (context.Current.Kind != SyntaxKind.EndToken && context.Current.Kind != SyntaxKind.EndOfFileToken)
        {
            var statement = ParseStatement(context);
            statements.Add(statement);
        }
        
        context.Match(SyntaxKind.EndToken, "After the body of a function declaration, an 'end' keyword is expected, ending the function declaration.");
        
        return new BlockStatementNode(start, statements);
    }

    private StatementNode ParseStatement(ParserContext context)
    {
        switch (context.Current.Kind)
        {
            default:
                return ParseExpressionStatement(context);
        }
    }
    
    private ExpressionStatementNode ParseExpressionStatement(ParserContext context)
    {
        var expression = ParseExpression(context);
        return new ExpressionStatementNode(expression.Location, expression);
    }

    private ExpressionNode ParseExpression(ParserContext context)
    {
        if ((context.Current.Kind == SyntaxKind.IdentifierToken && context.Peek(1).Kind == SyntaxKind.OpenParenthesisToken)
            || context.Current.Kind == SyntaxKind.IdentifierToken && context.Peek(1).Kind is SyntaxKind.ColonToken or SyntaxKind.DotToken && context.Peek(2).Kind == SyntaxKind.OpenParenthesisToken)
        {
            return ParseCallExpression(context);
        }
        
        return ParseValue(context); // TODO
    }

    private CallExpressionNode ParseCallExpression(ParserContext context)
    {
        SyntaxToken? @class = null;
        SyntaxToken? dot = null;
        SyntaxToken name;

        if (context.Current.Kind == SyntaxKind.IdentifierToken &&
            context.Peek(1).Kind == SyntaxKind.OpenParenthesisToken)
        {
            name = context.Read();
        }
        else if (context.Current.Kind == SyntaxKind.IdentifierToken &&
                 context.Peek(1).Kind is SyntaxKind.ColonToken or SyntaxKind.DotToken &&
                 context.Peek(2).Kind == SyntaxKind.OpenParenthesisToken)
        {
            @class = context.Read();
            dot = context.Read();
            name = context.Read();
        }
        else
        {
            throw new InvalidOperationException();
        }
        
        var openParen = context.Match(SyntaxKind.OpenParenthesisToken, "After the identifier of a function call, an opening parenthesis is expected, starting the argument list. If the function has no arguments, an empty argument list is expected.");
        var arguments = ParseArguments(context);
        var closeParen = context.Match(SyntaxKind.CloseParenthesisToken, "After the argument list of a function call, a closing parenthesis is expected, ending the argument list.");
        
        return new CallExpressionNode(new SourceLocation(name.Location.Line, SourceSpan.FromBounds(name.Location.Column.Start, closeParen.Location.Column.End)), @class, dot, name, openParen, closeParen, arguments);
    }

    private List<ExpressionNode> ParseArguments(ParserContext context)
    {
        var arguments = new List<ExpressionNode>();
        var parseNextArgument = true;
        
        while (parseNextArgument && context.Current.Kind != SyntaxKind.CloseParenthesisToken && context.Current.Kind != SyntaxKind.EndOfFileToken)
        {
            var argument = ParseExpression(context);
            arguments.Add(argument);
            
            if (context.Current.Kind == SyntaxKind.CommaToken)
            {
                context.Read();
            }
            else
            {
                parseNextArgument = false;
            }
        }
        
        return arguments;
    }

    private FunctionDeclarationNode ParseFunction(ParserContext context)
    {
        context.Match(SyntaxKind.FunctionToken);
        var name = context.Current.Kind == SyntaxKind.IdentifierToken ? context.Read() : new SyntaxToken(context.Current.Location, SyntaxKind.IdentifierToken, string.Empty);
        var openParen = context.Match(SyntaxKind.OpenParenthesisToken, "After the identifier of a function declaration, an opening parenthesis is expected, starting the parameter list. If the function has no parameters, an empty parameter list is expected. If the function should be anonymous, after the function keyword, an opening parenthesis is expected, starting the parameter list.");
        var parameters = ParseParameterList(context);
        var closeParen = context.Match(SyntaxKind.CloseParenthesisToken, "After the parameter list of a function declaration, a closing parenthesis is expected, ending the parameter list.");
        var returnType = context.Current.Kind == SyntaxKind.ColonToken ? ParseTypeDeclaration(context) : null;
        var body = ParseBlock(context);

        return new FunctionDeclarationNode(
            new SourceLocation(name.Location.Line,
                SourceSpan.FromBounds(name.Location.Column.Start,
                    returnType?.Location.Column.End ?? closeParen.Location.Column.End)),
            name, returnType, openParen, closeParen, parameters, body);
    }

    private List<ParameterNode> ParseParameterList(ParserContext context)
    {
        var parameters = new List<ParameterNode>();
        var parseNextParameter = true;
        
        while (context.Current.Kind != SyntaxKind.CloseParenthesisToken && context.Current.Kind != SyntaxKind.EndOfFileToken && parseNextParameter)
        {
            var parameter = ParseParameter(context);
            parameters.Add(parameter);
            
            if (context.Current.Kind == SyntaxKind.CommaToken)
            {
                context.NextToken();
            }
            else
            {
                parseNextParameter = false;
            }

            if (parameter.IsVararg)
            {
                parseNextParameter = false;
            }
        }
        
        return parameters;
    }

    private ParameterNode ParseParameter(ParserContext context)
    {
        if (context.Current.Kind == SyntaxKind.ThreeDotsToken)
        {
            return ParseVarargParameter(context);
        }
        
        var name = context.Match(SyntaxKind.IdentifierToken, "A parameter begins with an identifier, which is the name of the parameter. After the identifier, a colon is expected, separating the name from the type. After the colon, the type of the parameter is expected. If the parameter is optional, an equal sign is expected, separating the type from the default value. After the equal sign, the default value of the parameter is expected. If no colon is present, the parameter is of type 'any'. If no equal sign is present, the parameter is not optional.");
        var type = context.Current.Kind == SyntaxKind.ColonToken ? ParseTypeDeclaration(context) : null;
        var defaultValue = context.Current.Kind == SyntaxKind.EqualsToken ? ParseExpression(context) : null;
        
        var end = defaultValue?.Location.Column.End ?? type?.Location.Column.End ?? name.Location.Column.End;
        
        return new ParameterNode(new SourceLocation(name.Location.Line, SourceSpan.FromBounds(name.Location.Column.Start, end)), name, type, defaultValue);
    }

    private ParameterNode ParseVarargParameter(ParserContext context)
    {
        var dots = context.Read();
        
        var name = context.Current.Kind == SyntaxKind.IdentifierToken ? context.Match(SyntaxKind.IdentifierToken, "A vararg parameter begins with three dots [...]. After that an optional name can be set. If omitted Lua's arg will be used.") : null;
        var type = name != null && context.Current.Kind == SyntaxKind.ColonToken ? ParseTypeDeclaration(context) : null;
        
        var end = type?.Location.Column.End ?? name?.Location.Column.End ?? dots.Location.Column.End;

        name ??= new SyntaxToken(dots.Location, SyntaxKind.IdentifierToken, "...");
        
        return new ParameterNode(new SourceLocation(context.Current.Location.Line, SourceSpan.FromBounds(context.Current.Location.Column.Start, end)), name, type, null);
    }

    private SyntaxToken ParseTypeDeclaration(ParserContext context)
    {
        context.NextToken(); // skip the colon

        if (context.Current.Kind == SyntaxKind.IdentifierToken)
        {
            return context.Read();
        }
        
        // TODO the other types
        
        return context.Match(SyntaxKind.BadToken, "Expected a type declaration.");
    }

    private ValueNode ParseValue(ParserContext context)
    {
        if (context.Current.Kind is SyntaxKind.StringToken or SyntaxKind.NumberToken or SyntaxKind.BooleanToken)
        {
            return new ValueNode(context.Read());
        }
        
        // TODO the other types
        
        return new ValueNode(context.Match(SyntaxKind.BadToken, "Expected a value."));
    }
}