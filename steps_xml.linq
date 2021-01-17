<Query Kind="Statements" />

var xml = XElement.Load(@"c:\AssayDefinitions\StrepA_1.0.adf");
var query = from e in xml.Descendants("AssayStep")
			where (e.Parent.Name == ("Section") && e.Parent.Attribute("name").Value == "RunSetup")
			select e;

query.Dump();