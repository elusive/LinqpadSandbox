<Query Kind="Statements" />

/*
 * Various Datetime formats for C#
 */
 
var egdt = DateTime.Parse("09/01/2019 11:26:33pm");
egdt.Dump();

var fmt = "MMM dd, yyyy  h:mm:ss tt";
egdt.ToString(fmt).Dump();
