using System.Text;

namespace Kula.LuaGen.Generatables.Loops;

public class LuaForLoop : Symbol
{
    public LuaAssignment Init { get; }
    
    public double Limit { get; }
    
    public double Step { get; }
    
    public LuaScope Body { get; }

    public LuaForLoop(LuaAssignment init, double limit, double step)
    {
        Body = new LuaScope();
        Init = init;
        Limit = limit;
        Step = step;
    }

    public override string Generate()
    {
        var sb = new StringBuilder();
        
        sb.AppendLine($"for {Init.Generate()}, {Limit}, {Step} do");
        sb.AppendLine(Body.Generate());
        sb.AppendLine("end");
        
        return sb.ToString();
    }
}