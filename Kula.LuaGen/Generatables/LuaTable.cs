using System.Text;

namespace Kula.LuaGen.Generatables;

public class LuaTable : Symbol
{
    public List<LuaTableEntry> Entries { get; }
    
    public LuaTable(List<LuaTableEntry> entries)
    {
        Entries = entries;
    }

    public override string Generate()
    {
        var sb = new StringBuilder();
        
        sb.Append('{');
        
        var items = Entries.Select(x => x.Generate());
        sb.Append(string.Join(", ", items));
        
        sb.Append('}');
        
        return sb.ToString();
    }
}