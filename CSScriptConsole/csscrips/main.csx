#load "test.csx"

using System;

Math.Sqrt(3.2);
// Console.WriteLine(Math.Sqrt(3.2));
var a = new Spell();
a.GetHashCode();
// Console.WriteLine(a.GetHashCode());
a.GetInt();
// Console.WriteLine(a.GetInt());
a.GetInitAsync().Wait();
// Console.WriteLine(a.GetInitAsync().Result);