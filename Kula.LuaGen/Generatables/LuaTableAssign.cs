using System.Text;

namespace Kula.LuaGen.Generatables;

public class LuaTableAssign : Symbol
{
    public LuaVariable Table { get; }
    
    public LuaValue Key { get; }
    
    public LuaValue Value { get; }
    
    public LuaTableAssign(LuaVariable table, LuaValue key, LuaValue value)
    {
        Table = table;
        Key = key;
        Value = value;
    }
    
    public override string Generate()
    {
        var sb = new StringBuilder();

        sb.Append(Table.Name);
        sb.Append('[');
        sb.Append(Key.Generate());
        sb.Append("] = ");
        sb.Append(Value.Generate());
        
        return sb.ToString();
    }
}