#load "test.csx"
#r "..\CSScriptConsole\bin\Debug\netcoreapp2.0\CSScriptConsole.dll"

using System;
using CSScriptConsole;

var p2 = new Person("qwe", 23);
Console.WriteLine(p2.ToString());
Math.Sqrt(3.2);
// Console.WriteLine(Math.Sqrt(3.2));
var a = new Spell(p2);
a.GetHashCode();
// Console.WriteLine(a.GetHashCode());
a.GetInt();
// Console.WriteLine(a.GetInt());
a.GetInitAsync().Wait();
// Console.WriteLine(a.GetInitAsync().Result);