<Query Kind="Program">
  <NuGetReference>Microsoft.Windows.SDK.Contracts</NuGetReference>
  <Namespace>Windows.Devices.Enumeration</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <RuntimeVersion>3.1</RuntimeVersion>
</Query>

async Task Main()
{	
	FindDevices();
	Console.Read();
}

// You can define other methods, fields, classes and namespaces here
static Guid DnssdProtocol = new Guid("{4526e8c1-8aac-4153-9b16-55e86ada0e54}");


private void FindDevices()
{
	// Doc: https://docs.microsoft.com/en-us/windows/uwp/debug-test-perf/device-portal#service-features-and-notes
	var aqsFilter = "System.Devices.AepService.ProtocolId:={4526e8c1-8aac-4153-9b16-55e86ada0e54} AND System.Devices.Dnssd.Domain:=\"local\" AND System.Devices.Dnssd.ServiceName:=\"_ipp._tcp\"";
	var properties = new[] {
				"System.Devices.IpAddress",
				"System.Devices.Dnssd.HostName",
				"System.Devices.Dnssd.ServiceName",
				"System.Devices.Dnssd.PortNumber",
				"System.Devices.Dnssd.TextAttributes"
			};
	var watcher = DeviceInformation.CreateWatcher(aqsFilter, properties, DeviceInformationKind.AssociationEndpointService);
	watcher.Added += (sender, args) =>
	{
		Debug.WriteLine("Added: " + args.Name);
		var ipAddresses = args.Properties["System.Devices.IpAddress"] as string[];
		var remotePort = args.Properties["System.Devices.Dnssd.PortNumber"].ToString();
		var hostName = args.Properties["System.Devices.Dnssd.HostName"].ToString();
		var serviceName = args.Properties["System.Devices.Dnssd.ServiceName"].ToString();
		var textAttributes = args.Properties["System.Devices.Dnssd.TextAttributes"] as string[];
		Debug.WriteLine($"\t{ipAddresses.FirstOrDefault()}:{remotePort} : {string.Join(",", textAttributes)}");
		string securePort = textAttributes.Where(t => t.StartsWith("S=")).Select(t => t.Substring(2)).FirstOrDefault() ?? "0";
		string scheme = "http";
		if (securePort != "0")
		{
			scheme = "https";
			remotePort = securePort;
		}
		string devicePortalUrl = $"{scheme}://{ipAddresses.FirstOrDefault()}:{remotePort}";

	};
	watcher.Updated += (sender, args) => Debug.WriteLine("Updated: " + args.Id);
	watcher.EnumerationCompleted += (sender, args) => Debug.WriteLine("EnumerationCompleted");
	watcher.Start();
}