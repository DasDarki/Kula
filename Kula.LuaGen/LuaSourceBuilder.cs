using System.Text;
using Kula.LuaGen.Builders;
using Kula.LuaGen.Generatables;

namespace Kula.LuaGen;

/// <summary>
/// The source builder is the main component of the Lua generation process.
/// One source builder describes one Lua source file.
/// </summary>
public class LuaSourceBuilder : LuaScope
{
    /// <summary>
    /// Creates a new source builder.
    /// </summary>
    public static LuaSourceBuilder Create()
    {
        return new LuaSourceBuilder();
    }

    /// <summary>
    /// Creates a custom value which will just be inserted into the source code.
    /// </summary>
    /// <param name="code">The lua code.</param>
    /// <returns>The representing lua value.</returns>
    public static LuaValue CreateCustomValue(string code)
    {
        return new LuaValue(code, true);
    }

    /// <summary>
    /// Creates a new table builder.
    /// </summary>
    public static LuaTableBuilder CreateTable()
    {
        return new LuaTableBuilder();
    }
}