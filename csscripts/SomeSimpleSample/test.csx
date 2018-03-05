#load "test2.csx"
#r "..\..\CSScriptConsole\bin\Debug\netcoreapp2.0\CSScriptConsole.dll"

using System;
using System.Threading.Tasks;
using CSScriptConsole;

public class Spell
{

    public Spell(Person p)
    {
        Console.WriteLine(p.ToString());
    }

    public long GetValue(Guid id, int subid)
        => (long)id.GetHashCode() + subid.GetHashCode();

    public int GetInt() => Test2.GetIit();

    public Task<int> GetInitAsync() => Test2.GetInitAsync();
}