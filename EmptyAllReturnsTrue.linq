<Query Kind="Program" />

void Main()
{
	var badCodes = new[] { 1 };
	var list1 = new List<Blah> { new Blah { Code = 1 } };
	
	var hasBadCodes = list1.All(c => badCodes.Contains(c.Code));
	hasBadCodes.Dump("Before clear");
	list1.Clear();
	
	hasBadCodes = list1.All(c => badCodes.Contains(c.Code));
	hasBadCodes.Dump("After clear, just all");
	
	hasBadCodes = list1.Any() && list1.All(c => badCodes.Contains(c.Code));
	hasBadCodes.Dump("After clear, with any and all");
	
	var list2 = new List<string>() {};
	var emptyListAllLengthEquals1000 = list2.All(s => s.Length == 1000);
	emptyListAllLengthEquals1000.Dump("Empty List<string> All(s => s.Length == 1000");
}

// You can define other methods, fields, classes and namespaces here
public class Blah {
	public int Code {get; set;}
}