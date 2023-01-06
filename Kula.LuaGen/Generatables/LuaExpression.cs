namespace Kula.LuaGen.Generatables;

public class LuaExpression : Symbol
{
    public string Expression;

    public LuaExpression(string expr)
    {
        Expression = expr;
    }

    public override string Generate()
    {
        return Expression;
    }
}