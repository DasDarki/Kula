using System.Text;

namespace Kula.LuaGen.Generatables;

public class LuaFunctionCall : Symbol
{
    public LuaVariable? Owner { get; set; }
    
    public bool AsStatic { get; set; }
    
    public string Name { get; }
    
    public List<LuaValue> Arguments { get; }

    public LuaFunctionCall(string name)
    { 
        Name = name;
        Arguments = new List<LuaValue>();
    }

    public override string Generate()
    {
        var sb = new StringBuilder();

        if (Owner != null)
        {
            sb.Append(Owner.Name);
            sb.Append(!AsStatic ? ":" : ".");
        }
        
        sb.Append(Name);
        sb.Append('(');
        sb.Append(string.Join(", ", Arguments.Select(a => a.Generate())));
        sb.Append(')');
        
        return sb.ToString();
    }
}