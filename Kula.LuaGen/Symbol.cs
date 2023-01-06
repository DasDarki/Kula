namespace Kula.LuaGen;

/// <summary>
/// The symbol describes everything which will be transformed into lua code.
/// </summary>
public abstract class Symbol
{
    /// <summary>
    /// Generates the lua code.
    /// </summary>
    public abstract string Generate();
}