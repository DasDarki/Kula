namespace Kula.LuaGen.Generatables;

public class LuaReturn : Symbol
{
    public LuaValue Value { get; }
    
    public LuaReturn(LuaValue value)
    {
        Value = value;
    }
    
    public override string Generate()
    {
        return "return " + Value.Generate();
    }
}