using System.Text;

namespace Kula.LuaGen.Generatables.If;

public class LuaIf : Symbol
{
    public List<LuaIfBranch> IfBranches { get; }
    
    public LuaScope? ElseBranch { get; set; }

    public LuaIf()
    {
        IfBranches = new List<LuaIfBranch>();
    }

    public override string Generate()
    {
        var sb = new StringBuilder();

        for (var i = 0; i < IfBranches.Count; i++)
        {
            var branch = IfBranches[i];
            sb.Append((i == 0 ? "" : "else") + branch.Generate());
        }

        if (ElseBranch != null)
        {
            sb.AppendLine("else");
            sb.AppendLine(ElseBranch.Generate());
            sb.AppendLine("end");
        }

        return sb.ToString();
    }
}