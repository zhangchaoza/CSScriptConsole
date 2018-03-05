#define DEBUG

#if DEBUG

#r "CSScriptConsole"

#else

#r "..\..\CSScriptConsole\bin\Debug\netcoreapp2.0\CSScriptConsole.dll"

#endif

using System;
using CSScriptConsole;


var p2 = new Person("qwe", 23);
Console.WriteLine(p2.ToString());

var a = 0;