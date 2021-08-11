<Query Kind="Statements" />

var dt = new DateTime(2021, 6, 28, 6, 30, 0);
dt.Dump("Test datetime value");

// get diff from now
var diff = DateTime.Now - dt;
diff.TotalHours.Dump("difference from now");

// daily or weekly checks
var isWithinDaily = diff.TotalHours <= 24;
var isWithinWeekly = diff.TotalHours <= 168;