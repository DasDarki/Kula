using System.Text;

namespace Kula.LuaGen.Generatables;

public class LuaFunction : Symbol
{
    public bool IsLocal { get; set; }
    
    public string Name { get; set; }
    
    public List<string> Parameters { get; }
    
    public LuaScope Body { get; }
    
    public LuaFunction(string name)
    {
        Name = name;
        Parameters = new List<string>();
        Body = new LuaScope(1);
    }

    public override string Generate()
    {
        var sb = new StringBuilder();
        
        if (IsLocal)
            sb.Append("local ");
        
        sb.Append("function ");
        sb.Append(Name);
        sb.Append('(');
        
        for (var i = 0; i < Parameters.Count; i++)
        {
            sb.Append(Parameters[i]);
            
            if (i < Parameters.Count - 1)
                sb.Append(", ");
        }
        
        sb.Append(')');
        sb.AppendLine();
        sb.AppendLine(Body.Generate());
        sb.Append("end");
        
        return sb.ToString();
    }
}