<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <RemoveNamespace>System.Linq</RemoveNamespace>
  <RemoveNamespace>System.Linq.Expressions</RemoveNamespace>
</Query>

void Main()
{
	var s = new SensorEvent {
		sensor = "waste-cap-off-sensor",
		timestamp = DateTime.Now.Ticks,
		value = true
	};
	
	var jsn = JsonConvert.SerializeObject(s);
	var tkn = JObject.Parse(jsn);
	var isSensor = tkn.SelectToken("sensor");
	(isSensor != null).Dump("IsSensor");
	jsn.Dump("json");
}

// You can define other methods, fields, classes and namespaces here
public class SensorEvent
{
	public string sensor { get; set; }
	public long timestamp { get; set; }
	public bool value { get; set; }
}