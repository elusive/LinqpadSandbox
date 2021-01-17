<Query Kind="Program" />

void Main()
{
	Foo.allFoo().Dump();
	
	Foo.someFoo().Count().Dump();




}

// Define other methods and classes here
public class Foo {
	public Foo(string name, int age, bool active = false) {
		Name = name;
		Age = age;
		Active = active;
	}
	
	public string Name { get; set; }
	public int Age {get;set;}
	public bool Active {get;set;}
	
	public static IEnumerable<Foo> allFoo() {
			return new List<Foo>(){
			new Foo("Scooby", 22),
			new Foo("Daphne", 24, true),
			new Foo("Shagge", 25),
			new Foo("Velma", 37, true),
			new Foo("Fred", 41),
			new Foo("John", 44, true)
		};
	}
	
	public static IEnumerable<Foo> someFoo() {
		var some = allFoo().Where(f => f.Active);
		return some.Where(f => f.Age > 30);
	}
}


