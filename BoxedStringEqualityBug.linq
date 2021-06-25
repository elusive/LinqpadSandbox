<Query Kind="Statements" />

string original = "abcedf";

// literal
var a = original;
a.Dump("a");
var b = original;
b.Dump("b");
Console.Write($"{a} == {b}? : ");
Console.WriteLine(a == b);


// boxed only
object boxeda = a;
boxeda.Dump("boxeda");
object boxedb = b;
boxedb.Dump("boxedb");
Console.Write($"{boxeda} == {boxedb}? : ");
Console.WriteLine(boxeda == boxedb);

// boxed and constructed
object boxedAndFormata = string.Format(a);
boxedAndFormata.Dump("boxedAndFormata");
object boxedAndFormatb = string.Format(b);
boxedAndFormatb.Dump("boxedAndFormatb");

// == 
Console.Write($"{boxedAndFormata} == {boxedAndFormatb}? : ");
Console.WriteLine(boxedAndFormata == boxedAndFormatb);

// != 
Console.Write($"{boxedAndFormata} != {boxedAndFormatb}? : ");
Console.WriteLine(boxedAndFormata != boxedAndFormatb);

a.GetHashCode().Dump("a hashcode");
b.GetHashCode().Dump("b hashcode");
boxeda.GetHashCode().Dump("boxeda hashcode");
boxedb.GetHashCode().Dump("boxedb hashcode");
boxedAndFormata.GetHashCode().Dump("boxeda hashcode");
boxedAndFormatb.GetHashCode().Dump("boxedb hashcode");

