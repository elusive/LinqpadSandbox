<Query Kind="Statements" />

var flag = true;
var choices = new[] { "one", "two", "three" };
for (var i=0; i < 10; i++)
{
	choices[i % 3].Dump("choice");
	flag = !flag;
	flag.Dump("flag");
}