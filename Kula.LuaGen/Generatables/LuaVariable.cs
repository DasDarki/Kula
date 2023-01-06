using System.Text;

namespace Kula.LuaGen.Generatables;

public class LuaVariable : Symbol
{
    public string Name { get; }
    
    public LuaValue? Value { get; }
    
    public bool IsLocal { get; }

    public LuaVariable(string name, LuaValue? value, bool isLocal)
    {
        Name = name;
        Value = value;
        IsLocal = isLocal;
    }

    public override string Generate()
    {
        var sb = new StringBuilder();
        if (IsLocal)
            sb.Append("local ");
        
        sb.Append(Name);
        
        if (Value != null)
            sb.Append(" = ").Append(Value.Generate());
        
        return sb.ToString();
    }
}