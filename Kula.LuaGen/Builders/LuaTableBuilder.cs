using Kula.LuaGen.Generatables;

namespace Kula.LuaGen.Builders;

/// <summary>
/// The lua table builder helps creating lua table declarations.
/// </summary>
public class LuaTableBuilder
{
    private readonly List<LuaTableEntry> _entries;

    internal LuaTableBuilder()
    {
        _entries = new List<LuaTableEntry>();
    }
    
    /// <summary>
    /// Inserts a new key-value pair in the table.
    /// </summary>
    /// <param name="key">The key as <see cref="LuaValue"/> representation.</param>
    /// <param name="value">The value as <see cref="LuaValue"/> representation.</param>
    public LuaTableBuilder AddKeyValue(LuaValue key, LuaValue value)
    {
        _entries.Add(new LuaTableEntry(key, value));
        return this;
    }
    
    /// <summary>
    /// Inserts a new index-based value in the table.
    /// </summary>
    /// <param name="value">The value as <see cref="LuaValue"/> representation.</param>
    public LuaTableBuilder AddValue(LuaValue value)
    {
        _entries.Add(new LuaTableEntry(LuaValue.Nil, value));
        return this;
    }
    
    public LuaTableBuilder AddEntry(LuaTableEntry entry)
    {
        _entries.Add(entry);
        return this;
    }
    
    public LuaTableBuilder AddEntries(IEnumerable<LuaTableEntry> entries)
    {
        _entries.AddRange(entries);
        return this;
    }
    
    public LuaTable Build()
    {
        return new LuaTable(_entries);
    }
}