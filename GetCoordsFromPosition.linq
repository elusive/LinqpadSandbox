<Query Kind="Program" />

void Main()
{
	
	var t = Colors.red;
	
	Debug.Assert(Colors.purple.HasFlag(t));
	
}

// Define other methods and classes here
[Flags]
	public enum Colors
	{
		red = 1,
		blue = 2,
		yellow = 4,
		green = blue | yellow,
		purple = red | blue
	}