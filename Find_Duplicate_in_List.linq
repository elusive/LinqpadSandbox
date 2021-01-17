<Query Kind="Statements" />

var lst = new List<string> { "one", "two", "one", "three", "four", "five" };

foreach (var s in lst) 
{
	if (lst.GroupBy(x => x).Where(g => g.Count() > 1 && g.Key==s).Any())
	{
		s.Dump();
	}
}