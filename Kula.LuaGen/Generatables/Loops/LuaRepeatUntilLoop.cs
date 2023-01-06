using System.Text;

namespace Kula.LuaGen.Generatables.Loops;

public class LuaRepeatUntilLoop : Symbol
{
    public LuaExpression Condition { get; }
    
    public LuaScope Body { get; }
    
    public LuaRepeatUntilLoop(LuaExpression condition)
    {
        Condition = condition;
        Body = new LuaScope(1);
    }
    
    public override string Generate()
    {
        var sb = new StringBuilder();
        
        sb.AppendLine("repeat");
        sb.Append(Body.Generate());
        sb.AppendLine("until " + Condition.Generate());
        
        return sb.ToString();
    }
}