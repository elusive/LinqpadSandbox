<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Bonjour SDK\Samples\CS\DNSServiceBrowser\bin\Debug\Interop.Bonjour.dll</Reference>
  <Reference>C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.Management.dll</Reference>
  <Namespace>Bonjour</Namespace>
  <Namespace>System.Management</Namespace>
  <Namespace>LINQPad.Controls</Namespace>
</Query>

void Main()
{
	//var printerName = @"ipp://10.0.0.66/ipp/print";
	//var success = PrinterSettings.AddPrinter(printerName);
	//success.Dump();

	
	
	
	//ExecuteCommand("dns-sd", "-B");
	//result.Dump();
	
	items = new List<BrowseData>();
	browseList = new SelectBox(SelectBoxKind.ListBox, new[] {""});
	var dnsEvents = new DNSSDEventManager();
	dnsEvents.ServiceFound += new _IDNSSDEvents_ServiceFoundEventHandler(this.ServiceFound);
	var dns = new DNSSDService();
	dns.Browse(0, 0, "_http._tcp", null, dnsEvents);
	browseList.Dump();
	
	waiter = new AutoResetEvent(false);
	waiter.WaitOne();
}

// You can define other methods, fields, classes and namespaces here

SelectBox browseList;
List<BrowseData> items;
AutoResetEvent waiter;



void ExecuteCommand(string file, string command)
{
	string retMessage = String.Empty;
	lines = new List<string[]>();
	ProcessStartInfo startInfo = new ProcessStartInfo();
	Process p = new Process();

	startInfo.CreateNoWindow = true;
	startInfo.RedirectStandardOutput = true;
	startInfo.RedirectStandardError = true;

	startInfo.UseShellExecute = false;
	startInfo.Arguments = command;
	startInfo.FileName = file;

	p.EnableRaisingEvents = true;
	p.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_OutputDataReceived);
	p.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_ErrorDataReceived);
	p.Exited += new System.EventHandler(process_Exited);

	p.StartInfo = startInfo;
	p.Start();
	p.BeginErrorReadLine();
	p.BeginOutputReadLine();
	
	p.WaitForExit(5000);
	
	lines.Skip(2).Dump();
}
List<string[]> lines;
void process_Exited(object sender, EventArgs e)
{
	$"process exited with code {((Process)sender).ExitCode}\n".Dump();
}

void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
{
	$"{e.Data}\n".Dump();
}

void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
{
	lines.Add(Regex.Split(e.Data, @"\s+"));
}

public void ServiceFound
						(
						DNSSDService sref,
						DNSSDFlags flags,
						uint ifIndex,
						String serviceName,
						String regType,
						String domain
						)
{
	Console.WriteLine("here");
	int index = items.ToList().FindIndex(x => x.Name == serviceName);

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

		items.Add(data);
	}
	else
	{
		BrowseData data = (BrowseData)items[index];
		data.Refs++;
	}
	
	browseList = new SelectBox(
		SelectBoxKind.ListBox, 
		items.Select(i => i.Name).ToArray());
	browseList.Dump();
	waiter.Set();
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


class PrinterSettings
{
	private static ManagementScope oManagementScope = null;
	//Adds the Printer
	public static bool AddPrinter(string sPrinterName)
	{
		try
		{
			oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
			oManagementScope.Connect();

			ManagementClass oPrinterClass = new ManagementClass(new ManagementPath("Win32_Printer"), null);
			ManagementBaseObject oInputParameters = oPrinterClass.GetMethodParameters("AddPrinterConnection");

			oInputParameters.SetPropertyValue("Name", sPrinterName);

			oPrinterClass.InvokeMethod("AddPrinterConnection", oInputParameters, null);
			return true;
		}
		catch (Exception ex)
		{
			ex.Dump();
			return false;
		}
	}
	//Deletes the printer
	public static bool DeletePrinter(string sPrinterName)
	{
		oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
		oManagementScope.Connect();

		SelectQuery oSelectQuery = new SelectQuery();
		oSelectQuery.QueryString = @"SELECT * FROM Win32_Printer WHERE Name = '" + sPrinterName.Replace("\\", "\\\\") + "'";

		ManagementObjectSearcher oObjectSearcher = new ManagementObjectSearcher(oManagementScope, oSelectQuery);
		ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

		if (oObjectCollection.Count != 0)
		{
			foreach (ManagementObject oItem in oObjectCollection)
			{
				oItem.Delete();
				return true;
			}
		}
		return false;
	}
	//Renames the printer
	public static void RenamePrinter(string sPrinterName, string newName)
	{
		oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
		oManagementScope.Connect();

		SelectQuery oSelectQuery = new SelectQuery();
		oSelectQuery.QueryString = @"SELECT * FROM Win32_Printer WHERE Name = '" + sPrinterName.Replace("\\", "\\\\") + "'";

		ManagementObjectSearcher oObjectSearcher = new ManagementObjectSearcher(oManagementScope, oSelectQuery);
		ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

		if (oObjectCollection.Count != 0)
		{
			foreach (ManagementObject oItem in oObjectCollection)
			{
				oItem.InvokeMethod("RenamePrinter", new object[] { newName });
				return;
			}
		}

	}
	//Sets the printer as Default
	public static void SetDefaultPrinter(string sPrinterName)
	{
		oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
		oManagementScope.Connect();

		SelectQuery oSelectQuery = new SelectQuery();
		oSelectQuery.QueryString = @"SELECT * FROM Win32_Printer WHERE Name = '" + sPrinterName.Replace("\\", "\\\\") + "'";

		ManagementObjectSearcher oObjectSearcher = new ManagementObjectSearcher(oManagementScope, oSelectQuery);
		ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

		if (oObjectCollection.Count != 0)
		{
			foreach (ManagementObject oItem in oObjectCollection)
			{
				oItem.InvokeMethod("SetDefaultPrinter", new object[] { sPrinterName });
				return;

			}
		}
	}
	//Gets the printer information
	public static void GetPrinterInfo(string sPrinterName)
	{
		oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
		oManagementScope.Connect();

		SelectQuery oSelectQuery = new SelectQuery();
		oSelectQuery.QueryString = @"SELECT * FROM Win32_Printer WHERE Name = '" + sPrinterName.Replace("\\", "\\\\") + "'";

		ManagementObjectSearcher oObjectSearcher = new ManagementObjectSearcher(oManagementScope, @oSelectQuery);
		ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

		foreach (ManagementObject oItem in oObjectCollection)
		{
			Console.WriteLine("Name : " + oItem["Name"].ToString());
			Console.WriteLine("PortName : " + oItem["PortName"].ToString());
			Console.WriteLine("DriverName : " + oItem["DriverName"].ToString());
			Console.WriteLine("DeviceID : " + oItem["DeviceID"].ToString());
			Console.WriteLine("Shared : " + oItem["Shared"].ToString());
			Console.WriteLine("---------------------------------------------------------------");
		}
	}
	//Checks whether a printer is installed
	public bool IsPrinterInstalled(string sPrinterName)
	{
		oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
		oManagementScope.Connect();

		SelectQuery oSelectQuery = new SelectQuery();
		oSelectQuery.QueryString = @"SELECT * FROM Win32_Printer WHERE Name = '" + sPrinterName.Replace("\\", "\\\\") + "'";

		ManagementObjectSearcher oObjectSearcher = new ManagementObjectSearcher(oManagementScope, oSelectQuery);
		ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

		return oObjectCollection.Count > 0;
	}
}