using System.Text;

namespace Kula.LuaGen.Generatables.Loops;

public class LuaForEachLoop : Symbol
{
    public string KeyName { get; }
    
    public string ValueName { get; }
    
    public LuaValue Table { get; }
    
    public LuaScope Body { get; }
    
    public LuaForEachLoop(string keyName, string valueName, LuaValue table)
    {
        KeyName = keyName;
        ValueName = valueName;
        Table = table;
        Body = new LuaScope(1);
    }
    
    public override string Generate()
    {
        var sb = new StringBuilder();
        
        sb.AppendLine($"for {KeyName}, {ValueName} in pairs({Table.Generate()}) do");
        sb.Append(Body.Generate());
        sb.AppendLine("end");
        
        return sb.ToString();
    }
}