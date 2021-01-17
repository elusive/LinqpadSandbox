<Query Kind="Program">
  <Reference Relative="..\..\..\Downloads\Mono.Zeroconf-Mono.Zeroconf-0.9.0\bin\Mono.Zeroconf.dll">C:\Users\jgilliland\Downloads\Mono.Zeroconf-Mono.Zeroconf-0.9.0\bin\Mono.Zeroconf.dll</Reference>
  <Namespace>Mono.Zeroconf</Namespace>
  <RuntimeVersion>3.1</RuntimeVersion>
</Query>

void Main()
{
	Console.WriteLine("Hit ^C when you're bored waiting for responses.");
	Console.WriteLine();

	// Listen for events of some service type
	ServiceBrowser browser = new ServiceBrowser();
	browser.ServiceAdded += OnServiceAdded;
	browser.Browse(@interface, address_protocol, BrowseType, domain);

}

// You can define other methods, fields, classes and namespaces here
const string BrowseType = "_http._tcp";
const bool ResolveFound = true;
private uint @interface = 0;
private AddressProtocol address_protocol = AddressProtocol.Any;
private const string domain = "local";
private List<object> browseList = new List<object>();


private void OnServiceAdded(object o, ServiceBrowseEventArgs args)
{
	string.Format("*** Found name = '{0}', type = '{1}', domain = '{2}'",
		args.Service.Name,
		args.Service.RegType,
		args.Service.ReplyDomain).Dump();

	if (ResolveFound)
	{
		args.Service.Resolved += OnServiceResolved;
		args.Service.Resolve();
	}
}

private void OnServiceResolved(object o, ServiceResolvedEventArgs args)
{
	IResolvableService service = o as IResolvableService;
	string.Format("*** Resolved name = '{0}', host ip = '{1}', hostname = {2}, port = '{3}', " +
		"interface = '{4}', address type = '{5}'",
		service.FullName, service.HostEntry.AddressList[0], service.HostEntry.HostName, service.Port,
		service.NetworkInterface, service.AddressProtocol).Dump();

	ITxtRecord record = service.TxtRecord;
	int record_count = record != null ? record.Count : 0;
	var sb = new System.Text.StringBuilder();
	if (record_count > 0)
	{
		sb.Append(", TXT Record = [");
		for (int i = 0, n = record.Count; i < n; i++)
		{
			TxtRecordItem item = record.GetItemAt(i);
			sb.AppendFormat("{0} = '{1}'", item.Key, item.ValueString);
			if (i < n - 1)
			{
				sb.Append(", ");
			}
		}
		sb.Append("]");
	}
	else
	{
		sb.AppendLine("");
	}
	
	sb.ToString().Dump();
}
