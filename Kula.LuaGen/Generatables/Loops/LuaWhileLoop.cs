using System.Text;

namespace Kula.LuaGen.Generatables.Loops;

public class LuaWhileLoop : Symbol
{
    public LuaExpression Condition { get; }
    
    public LuaScope Body { get; }
    
    public LuaWhileLoop(LuaExpression condition)
    {
        Condition = condition;
        Body = new LuaScope(1);
    }
    
    public override string Generate()
    {
        var sb = new StringBuilder();
        
        sb.AppendLine("while " + Condition.Generate() + " do");
        sb.Append(Body.Generate());
        sb.AppendLine("end");
        
        return sb.ToString();
    }
}