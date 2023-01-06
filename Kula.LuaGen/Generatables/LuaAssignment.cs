using System.Text;

namespace Kula.LuaGen.Generatables;

public class LuaAssignment : Symbol
{
    public string Name { get; }
    
    public LuaValue Value { get; }
    
    public LuaAssignment(string name, LuaValue value)
    {
        Name = name;
        Value = value;
    }

    public override string Generate()
    {
        var sb = new StringBuilder();
        
        sb.Append(Name);
        sb.Append(" = ");
        sb.Append(Value.Generate());
        
        return sb.ToString();
    }
}