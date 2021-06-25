<Query Kind="Program" />

void Main()
{
	var OnboardOutputLocations = new List<OutputLocation> {
		new OutputLocation(1, true),
		new OutputLocation(2, true),
		new OutputLocation(3, false),
		new OutputLocation(4, true),
		new OutputLocation(5, false),
		new OutputLocation(6, false),
		new OutputLocation(7, false),
	}; 
	var requiredPositions = new[] {1,2,3,4};
	
	var oneToFourAvailable = false;
	oneToFourAvailable = requiredPositions.All(pos => OnboardOutputLocations.Exists(o => o.Position == pos && o.IsAvailable));
	oneToFourAvailable.Dump();
}

// You can define other methods, fields, classes and namespaces here
public class OutputLocation 
{
	public OutputLocation(int pos, bool avail) 
	{
		Position = pos;
		IsAvailable = avail;
	}
	public int Position { get; set; }
	public bool IsAvailable {get; set; }
}