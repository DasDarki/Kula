using Kula.LuaGen.Generatables;

namespace Kula.LuaGen.Builders;

public class LuaFunctionBuilder
{
    internal LuaFunction Function { get; }
    
    internal LuaFunctionBuilder()
    {
        Function = new LuaFunction("");
    }
    
    public LuaFunctionBuilder Local()
    {
        Function.IsLocal = true;
        return this;
    }
    
    public LuaFunctionBuilder Name(string name)
    {
        Function.Name = name;
        return this;
    }
    
    public LuaFunctionBuilder DescribeBody(Action<LuaScope> cb)
    {
        cb(Function.Body);
        return this;
    }
    
    public LuaFunctionBuilder AddParameter(string name)
    {
        if (Function.Parameters.LastOrDefault() == "...")
            throw new InvalidOperationException("Cannot add parameter after varargs");
        
        Function.Parameters.Add(name);
        return this;
    }
    
    public LuaFunctionBuilder AddVarargs()
    {
        Function.Parameters.Add("...");
        return this;
    }
}