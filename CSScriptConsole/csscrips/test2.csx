using System;
using System.Threading.Tasks;

public static class Test2
{
    static Random random = new Random();

    public static int GetIit()
    {
        return random.Next(100000);
    }

    public static async Task<int> GetInitAsync()
    {
        await Task.Delay(2000);
        return 557;
    }
}