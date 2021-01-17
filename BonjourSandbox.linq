<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Bonjour SDK\Samples\CS\DNSServiceBrowser\bin\Debug\Interop.Bonjour.dll</Reference>
  <Namespace>Bonjour</Namespace>
</Query>

void Main()
{
	eventManager = new DNSSDEventManager();
	eventManager.ServiceFound += new _IDNSSDEvents_ServiceFoundEventHandler(ServiceFound);
	eventManager.ServiceLost += new _IDNSSDEvents_ServiceLostEventHandler(ServiceLost);
	eventManager.ServiceResolved += new _IDNSSDEvents_ServiceResolvedEventHandler(ServiceResolved);
	eventManager.OperationFailed += new _IDNSSDEvents_OperationFailedEventHandler(OperationFailed);

	service = new DNSSDService();
	TryBrowse();
	//signal.WaitOne(5000);
	// service.Stop();
}

// You can define other methods, fields, classes and namespaces here
private Bonjour.DNSSDEventManager eventManager = null;
private Bonjour.DNSSDService service = null;
private Bonjour.DNSSDService browser = null;
private Bonjour.DNSSDService resolver = null;
private List<object> browseList = new List<object>();
private AutoResetEvent signal = new AutoResetEvent(false);
private const string BrowseType = "_ipp._tcp";


private bool TryBrowse()
{
	try
	{
		if (browser != null) {
			browser.Stop();
		}
		
		//
		// Selecting a service type will start a new browse operation.
		//
		browser = service.Browse(0, 0, BrowseType, null, eventManager);
		return true;
	}
	catch (Exception e)
	{
		e?.Dump();
		return false;
	}
}

private bool TryResolve(BrowseData data) 
{
	try
	{
		resolver?.Stop();
		
		resolver = service.Resolve(0, data.InterfaceIndex, data.Name, data.Type, data.Domain, eventManager);
		return true;
	}	
	catch(Exception e) 
	{
		e?.Dump();
		return false;
	}
}


public void ServiceFound(DNSSDService sref,
						DNSSDFlags flags,
						uint ifIndex,
						String serviceName,
						String regType,
						String domain)
{
	int index = browseList.IndexOf(serviceName);

	//
	// Check to see if we've seen this service before. If the machine has multiple
	// interfaces, we could potentially get called back multiple times for the
	// same service. Implementing a simple reference counting scheme will address
	// the problem of the same service showing up more than once in the browse list.
	//
	if (index == -1)
	{
		BrowseData data = new BrowseData();

		data.InterfaceIndex = ifIndex;
		data.Name = serviceName;
		data.Type = regType;
		data.Domain = domain;
		data.Refs = 1;

		browseList.Add(data);
	}
	else
	{
		BrowseData data = (BrowseData)browseList[index];
		data.Refs++;
	}
	
	browseList.Dump();
	
	TryResolve((BrowseData)browseList[0]);
}

public void ServiceLost
						(
						DNSSDService sref,
						DNSSDFlags flags,
						uint ifIndex,
						String serviceName,
						String regType,
						String domain
						)
{}

public void ServiceResolved
						(
						DNSSDService sref,
						DNSSDFlags flags,
						uint ifIndex,
						String fullName,
						String hostName,
						ushort port,
						TXTRecord txtRecord
						)
{
	ResolveData data = new ResolveData();

	data.InterfaceIndex = ifIndex;
	data.FullName = fullName;
	data.HostName = hostName;
	data.Port = port;
	data.TxtRecord = txtRecord;

	//
	// Don't forget to stop the resolver. This eases the burden on the network
	//
	resolver.Stop();
	resolver = null;

	data.Dump();
	RenderTxtRecord(data);
}

public void OperationFailed
						(
						DNSSDService sref,
						DNSSDError error
						)
{
	Console.WriteLine("Operation failed: error code: " + error, "Error");
}

public void RenderTxtRecord(ResolveData resolveData)
{
	var sb = new System.Text.StringBuilder();
	if (resolveData.TxtRecord != null)
	{
		for (uint idx = 0; idx < resolveData.TxtRecord.GetCount(); idx++)
		{
			String key;
			Byte[] bytes;

			key = resolveData.TxtRecord.GetKeyAtIndex(idx);
			bytes = (Byte[])resolveData.TxtRecord.GetValueAtIndex(idx);

			if (key.Length > 0)
			{
				String val = "";

				if (bytes != null)
				{
					val = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
				}

				sb.AppendLine(key + "=" + val);
			}
		}
		sb.ToString().Dump();
	}
}

public class BrowseData
{
	public uint InterfaceIndex;
	public String Name;
	public String Type;
	public String Domain;
	public int Refs;

	public override String
	ToString()
	{
		return Name;
	}

	public override bool
	Equals(object other)
	{
		bool result = false;

		if (other != null)
		{
			result = (this.Name == other.ToString());
		}

		return result;
	}

	public override int
	GetHashCode()
	{
		return Name.GetHashCode();
	}
};


public class ResolveData
{
	public uint InterfaceIndex;
	public String FullName;
	public String HostName;
	public int Port;
	public TXTRecord TxtRecord;

	public override String
		ToString()
	{
		return FullName;
	}
};
