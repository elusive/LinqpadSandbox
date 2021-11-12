<Query Kind="Statements" />

double original = 1.51;
int casted = (int)original;
var rounded = Math.Round(original);

original.Dump("original value");
casted.Dump("casted double ==> int");
rounded.Dump("rounded double");