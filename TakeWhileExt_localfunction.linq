<Query Kind="Program" />

void Main()
{
	var s = new List<string> {
		"Hello", "World", "Foo", "Bar", "john", "was", "here", "1234"
	};
	
	var longerThan3 = s.TakeWhile(x => x.Length > 3);
	longerThan3.Dump();
}

public static class Ext
{
	public static IEnumerable<string> TakeWhile(this IEnumerable<string> src, Func<string, bool>  predicate)
	{
		return _();
		IEnumerable<string> _()
		{
			foreach(var item in src)
			{
				if (!predicate(item)) yield break;
				yield return item;
			}
		}
	}
}