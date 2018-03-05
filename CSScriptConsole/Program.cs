using System;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Diagnostics;
using Microsoft.CodeAnalysis.Scripting;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis.Scripting.Hosting;

namespace CSScriptConsole
{
    class Program
    {
        static string scriptrootpath = Path.Combine(@"D:\code\GitHub\CSScriptConsole", "csscripts");
        static string scrippath = Path.Combine(scriptrootpath, "SomeSimpleSample");
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
                    // var r = RunFileAsync().Result;
                    var r = RunFile2Async().Result;
                    // var r = RunScriptUpdate();
                    // var r = RunCustomAssemblyLoder();
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
                var state = await script.RunAsync();
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

        private static async Task<TimeSpan> RunFile2Async()
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
                    .WithFilePath(Path.Combine(scrippath, "test3.csx"))
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
                var state = await script.RunAsync();
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
        private static TimeSpan RunMutiThread()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var po = new ParallelOptions
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                };
                var option = ScriptOptions.Default
                        .WithFilePath(Path.Combine(scrippath, "main.csx"))
                        .WithFileEncoding(Encoding.UTF8);
                String code = "#load \"main.csx\"";
                // String code = @"
                // using System;
                // var a=1;
                // if(a==1)
                // {
                //     a=2;
                //     Console.WriteLine(0);
                // }
                // else
                // {
                //     Console.WriteLine(1);
                // }
                // ";
                var script = CSharpScript.Create(code, options: option);
                script.Compile();

                Parallel.For(0, 500000, po, i =>
                // for (int i = 0; i < 1000; i++)
                {
                    var state = script.RunAsync(null).Result;
                }
                )
                ;
            }
            finally
            {
                stopwatch.Stop();
            }
            return stopwatch.Elapsed;
        }

        static Script scriptForRunScriptUpdate;

        private static TimeSpan RunScriptUpdate()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                if (null == scriptForRunScriptUpdate)
                {
                    var option = ScriptOptions.Default
                            .WithFilePath(Path.Combine(scrippath, "main.csx"))
                            .WithFileEncoding(Encoding.UTF8);
                    String code = @"
                    #load ""main.csx""
                    ";
                    scriptForRunScriptUpdate = CSharpScript.Create(code, options: option);
                }
                scriptForRunScriptUpdate.Compile();
                var state = scriptForRunScriptUpdate.RunAsync(null).Result;
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

        private static TimeSpan RunCustomAssemblyLoder()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using (var loader = new InteractiveAssemblyLoader())
                {
                    if (AssemblyIdentity.TryParseDisplayName("CSScriptConsole, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", out AssemblyIdentity cccassemblyid))
                    {
                        loader.RegisterDependency(cccassemblyid, Path.Combine(AppContext.BaseDirectory, "CSScriptConsole.dll"));
                    }
                    var option = ScriptOptions.Default
                            .WithFilePath(Path.Combine(scriptrootpath, "CustomizeAssemblyLoading", "main.csx"))
                            .WithFileEncoding(Encoding.UTF8);
                    String code = "#load \"main.csx\"";
                    var script = CSharpScript.Create(code, options: option, assemblyLoader: loader);
                    var c = script.Compile();
                    var state = script.RunAsync(null).Result;
                }
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

    public class Person
    {
        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"{Name}:{Age}";
        }
    }
}
