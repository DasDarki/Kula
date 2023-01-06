using System.Text;

namespace Kula.LuaGen.Generatables;

public class LuaTableEntry : Symbol
{
    public LuaValue Key { get; }
    
    public LuaValue Value { get; }
    
    public LuaTableEntry(LuaValue key, LuaValue value)
    {
        Key = key;
        Value = value;
    }
    
    public override string Generate()
    {
        var sb = new StringBuilder();

        if (!Key.IsNull)
        {
            sb.Append('[');
            sb.Append(Key.Generate());
            sb.Append("] = ");
        }
        
        sb.Append(Value.Generate());
        
        return sb.ToString();
    }
}