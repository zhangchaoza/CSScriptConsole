using System;
using System.Threading.Tasks;

public static class Test2
{
    static Random random = new Random();

    public static int GetIit() => random.Next(100000);

    public static async Task<int> GetInitAsync()
    {
        await SomeJob();
        return 557;
    }

    private static Task SomeJob()
        => Task.Run(() => Task.CompletedTask
        // Console.WriteLine("Do something async.")
        );
}