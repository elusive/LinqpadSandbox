<Query Kind="Program">
  <NuGetReference>System.IO.FileSystem.AccessControl</NuGetReference>
  <Namespace>System.Security.AccessControl</Namespace>
  <Namespace>System.Security.Principal</Namespace>
</Query>

void Main()
{
	var pathReadOnly = "C:\\Temp\\ro";
	var dirPath = "C:\\Temp\\";
	
	var isDirRoWritable = IsDirectoryWritable(pathReadOnly);
	var isDirWritable = IsDirectoryWritable(dirPath);
	
	isDirRoWritable.Dump("Read Only Path");
	isDirWritable.Dump("Normal Path");

	// some other code that will iterate through the rules and output access
	string directory = @"C:\Temp\ro";

	DirectoryInfo di = new DirectoryInfo(directory);

	DirectorySecurity ds = di.GetAccessControl();

	foreach (AccessRule rule in ds.GetAccessRules(true, true, typeof(NTAccount)))
	{
		Console.WriteLine("Identity = {0}; Access = {1}",
					  rule.IdentityReference.Value, rule.AccessControlType);
	}
}

// You can define other methods, fields, classes and namespaces here

public bool IsFileWritable(string path)
{
	try
	{
		if (string.IsNullOrEmpty(path) || !File.Exists(path)) return false;

		var fi = new FileInfo(path);
		var ac = fi.GetAccessControl();
		return ac != null;
	}
	catch(Exception)
	{
		return false;
	}
}

public bool IsDirectoryWritable(string path) 
{
	try
	{
		var writeAllow = false;
    var writeDeny = false;
    var accessControlList = new DirectoryInfo(path).GetAccessControl();
		if (accessControlList == null)
			return false;
		var accessRules = accessControlList.GetAccessRules(true, true,
									typeof(System.Security.Principal.SecurityIdentifier));
		if (accessRules == null)
			return false;

		foreach (FileSystemAccessRule rule in accessRules)
		{
			if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
				continue;

			if (rule.AccessControlType == AccessControlType.Allow)
				writeAllow = true;
			else if (rule.AccessControlType == AccessControlType.Deny)
				writeDeny = true;
		}

		return writeAllow && !writeDeny;
	}
	catch (Exception)
	{
		return false;
	}
}