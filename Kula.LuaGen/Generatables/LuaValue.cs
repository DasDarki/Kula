using System.Globalization;

namespace Kula.LuaGen.Generatables;

public class LuaValue : Symbol
{
    public static readonly LuaValue Nil = new(null);
    
    public object? Value { get; }
    
    public bool IsCustom { get; }
    
    public static implicit operator LuaValue(string o) => new(o);
    public static implicit operator LuaValue(int o) => new(o);
    public static implicit operator LuaValue(double o) => new(o);
    public static implicit operator LuaValue(bool o) => new(o);
    public static implicit operator LuaValue(LuaTable o) => new(o);
    public static implicit operator LuaValue(LuaFunction o) => new(o);
    public static implicit operator LuaValue(LuaExpression o) => new(o);
    public static implicit operator LuaValue(LuaVariable o) => new(o);

    public LuaValue(object? value, bool isCustom = false)
    {
        Value = value;
        IsCustom = isCustom;
    }
    
    public bool IsNull => Value == null;
    
    public override string Generate()
    {
        if (IsCustom)
            return Value as string ?? "";
        
        return Value switch
        {
            null => "nil",
            string s => $"\"{s}\"",
            int i => i.ToString(),
            double d => d.ToString(CultureInfo.InvariantCulture),
            bool b => b ? "true" : "false",
            LuaTable t => t.Generate(),
            LuaFunction f => f.Generate(),
            LuaExpression e => e.Generate(),
            LuaVariable v => v.Name,
            _ => throw new NotSupportedException("The type of the value is not supported.")
        };
    }
}