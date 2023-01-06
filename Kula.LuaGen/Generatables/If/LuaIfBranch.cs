using System.Text;

namespace Kula.LuaGen.Generatables.If;

public class LuaIfBranch : Symbol
{
    public LuaExpression Condition { get; }
    
    public LuaScope Body { get; }
    
    public LuaIfBranch(LuaExpression condition)
    {
        Condition = condition;
        Body = new LuaScope(1);
    }

    public override string Generate()
    {
        var sb = new StringBuilder();
        
        sb.Append("if ");
        sb.Append(Condition.Generate());
        sb.Append(" then");
        sb.AppendLine();
        sb.Append(Body.Generate());
        sb.Append("end");
        
        return sb.ToString();
    }
}