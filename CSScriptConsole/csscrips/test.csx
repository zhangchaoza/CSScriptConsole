#load "test2.csx"

using System;
using System.Threading.Tasks;

public class Spell
{

    public long GetValue(Guid id, int subid)
        => (long)id.GetHashCode() + subid.GetHashCode();

    public int GetInt() => Test2.GetIit();

    public Task<int> GetInitAsync() => Test2.GetInitAsync();
}