<Query Kind="Program" />

void Main()
{
	var i = new List<IEnumerable<int>> { Enumerable.Range(1, 2), Enumerable.Range(1, 5) };
	
	var results = i.CartesianProduct();
	
	results.Dump();
	
	//i.Select((range, idx) => range.Select(x => i[idx + 1].Select(y => new List<int> {x, y}))).Dump();
}

// You can define other methods, fields, classes and namespaces here
public static class DeviceLocationExtensions
{
	public static IEnumerable<IEnumerable<int>> CartesianProduct(this IEnumerable<IEnumerable<int>> sequences)
	{
		// start by creating empty existing product result:
		// base case: 
		IEnumerable<IEnumerable<int>> result = new[] { Enumerable.Empty<int>() };

		foreach (var sequence in sequences)
		{
			// do not close over loop var (fixed in later versions of C#
			var s = sequence;
			
			// use select many to build new product 
			// from existing one and next sequence:
			result = 
				from seq in result
				from item in s
				select seq.Concat(new[] { item });
		}
		
		// return accumulated product:
		return result;
	}
}