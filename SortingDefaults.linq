<Query Kind="Statements" />

var dateOct3 = DateTime.Parse("10/3/2020");
var dateAug14 = DateTime.Parse("8/14/2020");
var dateMay6 = DateTime.Parse("5/6/2020");

var list = new List<DateTime> {
	dateOct3,
	dateMay6,
	dateAug14
	};
	
"Unsorted".Dump();
"===================".Dump();
list.Dump();

"".Dump();
"Sorted default".Dump();
list.OrderBy(x => x).Dump();

"".Dump();
"Sorted desc".Dump();
list.OrderByDescending(x => x).Dump();