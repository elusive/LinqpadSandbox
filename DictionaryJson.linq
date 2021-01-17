<Query Kind="Statements">
  <Namespace>System.Text.Json</Namespace>
</Query>

var filter = new Dictionary<string, IDictionary<string, bool>> {
				{ "ImageID", new Dictionary<string, bool> { {"423qlgk3qrflk34f", true} } }
			};
JsonSerializer.Serialize(filter).Dump();