using System.Text;
using Kula.LuaGen.Builders;
using Kula.LuaGen.Generatables.Loops;

namespace Kula.LuaGen.Generatables;

/// <summary>
/// A lua scope describes an area which holds a list of <see cref="Symbol"/>s.
/// </summary>
public class LuaScope
{
    /// <summary>
    /// The current scopes indentation.
    /// </summary>
    internal int Indent { get; private set; }
    
    protected readonly List<Symbol> Children;

    public LuaScope(int indent = 0)
    {
        Children = new List<Symbol>();
        Indent = indent;
    }
    
    /// <summary>
    /// Increases the indent of this scope.
    /// </summary>
    internal LuaScope Intend(int indent)
    {
        Indent += indent;
        return this;
    }

    /// <summary>
    /// Begins a new function. If no name is set, the function will be anonymous.
    /// </summary>
    public LuaFunctionBuilder Function()
    {
        var builder = new LuaFunctionBuilder();
        builder.Function.Body.Intend(Indent);
        Children.Add(builder.Function);
        return builder;
    }
    
    /// <summary>
    /// Creates a new variable in the current scope.
    /// </summary>
    /// <param name="name">The name of the variable.</param>
    /// <param name="value">The optional default value of the variable.</param>
    public LuaVariable Var(string name, LuaValue? value = null)
    {
        var variable = new LuaVariable(name, value, false);
        Children.Add(variable);

        return variable;
    }
    
    /// <summary>
    /// Creates a new local variable in the current scope.
    /// </summary>
    /// <param name="name">The name of the variable.</param>
    /// <param name="value">The optional default value of the variable.</param>
    public LuaVariable LocalVar(string name, LuaValue? value = null)
    {
        var variable = new LuaVariable(name, value, true);
        Children.Add(variable);

        return variable;
    }

    /// <summary>
    /// Creates a return statement.
    /// </summary>
    /// <param name="value">The value to be returned.</param>
    public LuaScope Return(LuaValue value)
    {
        Children.Add(new LuaReturn(value));
        
        return this;
    }
    
    /// <summary>
    /// Creates a variable assignment statement.
    /// </summary>
    /// <param name="variable">The variable.</param>
    /// <param name="value">The new value.</param>
    public LuaScope AssignVar(LuaVariable variable, LuaValue value)
    {
        Children.Add(new LuaAssignment(variable.Name, value));
        
        return this;
    }
    
    /// <summary>
    /// Creates a table assignment statement.
    /// </summary>
    /// <param name="table">The variable of the table.</param>
    /// <param name="key">The key of the wanted table entry.</param>
    /// <param name="value">The new value.</param>
    public LuaScope AssignTable(LuaVariable table, LuaValue key, LuaValue value)
    {
        Children.Add(new LuaTableAssign(table, key, value));
        
        return this;
    }

    /// <summary>
    /// Begins a if-branching statement.
    /// </summary>
    /// <param name="condition">The condition for the first if.</param>
    /// <param name="bodyDescriptor">A callback which gets called to describe the if body.</param>
    /// <returns>The builder to create the rest branching.</returns>
    public LuaIfBuilder If(string condition, Action<LuaScope> bodyDescriptor)
    {
        var ifStatement = new LuaIfBuilder(condition, bodyDescriptor, this);
        Children.Add(ifStatement.If);

        return ifStatement;
    }
    
    /// <summary>
    /// Creates a while-loop statement.
    /// </summary>
    /// <param name="condition">The condition for the while loop.</param>
    /// <param name="bodyDescriptor">A callback which gets called to describe the if body.</param>
    public LuaScope While(string condition, Action<LuaScope> bodyDescriptor)
    {
        var whileStatement = new LuaWhileLoop(new LuaExpression(condition));
        whileStatement.Body.Intend(Indent);
        Children.Add(whileStatement);
        
        bodyDescriptor(whileStatement.Body);
        return this;
    }

    /// <summary>
    /// Creates a repeat-until-loop statement.
    /// </summary>
    /// <param name="condition">The condition for the while loop.</param>
    /// <param name="bodyDescriptor">A callback which gets called to describe the if body.</param>
    public LuaScope RepeatUntil(string condition, Action<LuaScope> bodyDescriptor)
    {
        var repeatStatement = new LuaRepeatUntilLoop(new LuaExpression(condition));
        repeatStatement.Body.Intend(Indent);
        Children.Add(repeatStatement);
        
        bodyDescriptor(repeatStatement.Body);
        return this;
    }
    
    /// <summary>
    /// Creates for-loop statement.
    /// </summary>
    /// <param name="variable">The name of variable to count.</param>
    /// <param name="initial">The initial value for the variable.</param>
    /// <param name="limit">The limit of the for loop.</param>
    /// <param name="step">The step to increment/decrement.</param>
    /// <param name="bodyDescriptor">A callback which gets called to describe the if body.</param>
    public LuaScope For(string variable, double initial, double limit, double step, Action<LuaScope> bodyDescriptor)
    {
        var forStatement = new LuaForLoop(new LuaAssignment(variable, initial), limit, step);
        forStatement.Body.Intend(Indent);
        Children.Add(forStatement);
        
        bodyDescriptor(forStatement.Body);
        return this;
    }
    
    /// <summary>
    /// Creates a for-each-loop statement with pairs.
    /// </summary>
    /// <param name="keyName">The name of the key variable.</param>
    /// <param name="valueName">The name of the value variable.</param>
    /// <param name="table">The table to iterate. Only variables or tables are allowed.</param>
    /// <param name="bodyDescriptor">A callback which gets called to describe the if body.</param>
    public LuaScope ForEach(string keyName, string valueName, LuaValue table, Action<LuaScope> bodyDescriptor)
    {
        var forStatement = new LuaForEachLoop(keyName, valueName, table);
        forStatement.Body.Intend(Indent);
        Children.Add(forStatement);
        
        bodyDescriptor(forStatement.Body);
        return this;
    }

    /// <summary>
    /// Generates the lua code for this scope.
    /// </summary>
    public string Generate()
    {
        var sb = new StringBuilder();
        
        foreach (var child in Children)
        {
            sb.AppendLine(GetIndent() + child.Generate());
        }
        
        return sb.ToString();
    }
    
    private string GetIndent()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < Indent; i++)
        {
            sb.Append("    ");
        }

        return sb.ToString();
    }
}