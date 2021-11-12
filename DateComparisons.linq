<Query Kind="Statements" />

// closed vial expiry
var exp = new DateTime(2021, 9, 9);

// onboard (open vial) expiry
var onb = new DateTime(2021, 9, 15);

var today = DateTime.Today;

var expIsLessThanToday = exp < today;
var expIsGreaterThanToday = exp > today;

expIsLessThanToday.Dump("Is Less than Today");
expIsGreaterThanToday.Dump("Is Greater than Today");

$"{exp} is greater than {today}".Dump();