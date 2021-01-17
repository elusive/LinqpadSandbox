<Query Kind="Program" />

void Main()
{
	var names = Enum.GetNames(typeof(MyEnum));
	names.Dump();
	var code = MyEnum.Two;
	var name = Enum.GetName(typeof(MyEnum), code);
	name.Dump();
	var nameFromNames = names[(int)code - 1];
	nameFromNames.Dump();
}

// You can define other methods, fields, classes and namespaces here
enum MyEnum 
{
	One = 1,
	Two = 2,
	Three = 3
}