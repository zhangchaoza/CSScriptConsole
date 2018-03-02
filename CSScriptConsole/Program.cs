using System;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Diagnostics;
using Microsoft.CodeAnalysis.Scripting;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static string scrippath = Path.Combine(AppContext.BaseDirectory, "csscrips");
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Is continue? [Y]es/[N]o");
                var iscontinue = Console.ReadLine();
                if ("Y" == iscontinue.ToUpper())
                {
                    // Console.WriteLine("Your file:");
                    // var code = Console.ReadLine();
                    // var r = RunAsync(string.IsNullOrEmpty(code) ? null : code).Result;
                    var r = RunFileAsync().Result;
                    Console.WriteLine($"耗时:{r}");
                }
                else if ("N" == iscontinue.ToUpper())
                {
                    break;
                }
            }
            Console.WriteLine("\nPress 任意键退出");
            Console.ReadLine();
        }

        private static async Task<TimeSpan> RunAsync(string code = "1+2")
        {
            if (string.IsNullOrEmpty(code))
            {
                code = "1+2";
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            #region snippet1

            int result_int = await CSharpScript.EvaluateAsync<int>(code);

            #endregion

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        private static async Task<TimeSpan> RunFileAsync()
        {
            // if (string.IsNullOrEmpty(file))
            // {
            //     file = "test";
            // }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                #region snippet2

                var option = ScriptOptions.Default
                    .WithFilePath(Path.Combine(scrippath, "main.csx"))
                    .WithFileEncoding(Encoding.UTF8);
                // .AddReferences(typeof(Console).Assembly)
                // .WithImports("System");

                // String code =
                // $@"
                // #load ""{file}.csx""
                // var a=new Spell();
                // Console.WriteLine(a.GetHashCode());
                // Console.WriteLine(a.GetInt());
                // Console.WriteLine(a.GetInitAsync().Result);
                // ";
                String code = "#load \"main.csx\"";
                var script = CSharpScript.Create(code, options: option);
                script.Compile();
                var state = await script.RunAsync(null);
                // var state = await CSharpScript.RunAsync(code, options: option);

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }
            finally
            {
                stopwatch.Stop();
            }
            return stopwatch.Elapsed;
        }
    }
}
