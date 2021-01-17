<Query Kind="Statements" />

var items = new List<string> {
	"item1",
	"anotheritem",
	"item2",
	"secondItem",
	"third",
	"yetonemore"
};

var itemsWithDupe = new List<string> {
	"item1",
	"anotheritem",
	"item1",
	"secondItem",
	"third",
	"yetonemore"
};

Func<List<string>, bool> CheckForDupe = (lst) =>
{
	var isDuplicate = lst.Distinct().Count() != lst.Count();
	return isDuplicate;
};

CheckForDupe(items).Dump();
CheckForDupe(itemsWithDupe).Dump();




