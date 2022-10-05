<Query Kind="Statements" />

var l1 = new List<int> { 2, 3 };
var l2 = new List<int> { 3 };

var hasMatch = l1.Intersect(l2).Any();
hasMatch.Dump();
