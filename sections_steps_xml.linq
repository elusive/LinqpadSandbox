<Query Kind="Statements" />

var xml = XElement.Load(@"c:\AssayDefinitions\StrepA_1.0.adf");
var query = from e in xml.Descendants("Section")
			select e;

query.Dump();

foreach (var s in query) {
	var step = from el in s.Descendants("AssayStep")
				select el;
	step.Dump();
}