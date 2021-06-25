<Query Kind="Statements" />

const string marker = "minutes";
var pattern = new Regex($"([0-9]+)\\s(?:{marker})");
var match = pattern.Match("over incubation by 8 minutes");
if (!match.Success || match.Groups.Count != 2)
{
	throw new InvalidOperationException("Unexpected description for over incubation. Unable to extract number of minutes.");
}

var minutes = match.Groups[1].Value;
minutes.Dump();


match.Dump();
match.Groups.Dump();