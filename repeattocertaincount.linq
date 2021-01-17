<Query Kind="Statements" />

var x = new[] {31, 32};
var y = Enumerable.Repeat(x, 5).SelectMany(v => v).Take(4).ToArray();

y.Dump();
