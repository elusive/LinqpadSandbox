<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var s = "{\"adapter\": \"1\"}";
var x = (JObject)JsonConvert.DeserializeObject(s);
var y = x.ToObject<Dictionary<string, string>>();
y.Dump();
x.GetType().Dump();