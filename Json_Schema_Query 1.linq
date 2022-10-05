<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var s = File.ReadAllText(@"C:\Users\jgilliland\temp\recommender-v2\spoolup-v1.json");
var jobject = JObject.Parse(s);
var items = (JArray)jobject["items"];
var contents = from x in items select x["content"];
jobject.Dump();
contents.Dump();