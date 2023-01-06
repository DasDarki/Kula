using Kula.LuaGen.Generatables;
using Kula.LuaGen.Generatables.If;

namespace Kula.LuaGen.Builders;

public class LuaIfBuilder
{
    internal LuaIf If { get; }

    private readonly LuaScope _scope;
    
    internal LuaIfBuilder(string condition, Action<LuaScope> cb, LuaScope scope)
    {
        _scope = scope;
        If = new LuaIf();

        var branch = new LuaIfBranch(new LuaExpression(condition));
        branch.Body.Intend(_scope.Indent);
        cb(branch.Body);
        If.IfBranches.Add(branch);
    }
    
    public LuaIfBuilder ElseIf(string condition, Action<LuaScope> cb)
    {
        var branch = new LuaIfBranch(new LuaExpression(condition));
        branch.Body.Intend(_scope.Indent);
        cb(branch.Body);
        If.IfBranches.Add(branch);
        return this;
    }
    
    public LuaIfBuilder Else(Action<LuaScope> cb)
    {
        var branch = new LuaScope(1 + _scope.Indent);
        cb(branch);
        If.ElseBranch = branch;
        return this;
    }
}