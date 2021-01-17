<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <GACReference>System.Threading, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</GACReference>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>


void Main()
{
	Console.WriteLine("Starting:");
	
	using (var mre = new ManualResetEvent(false))
	{
		Spin(15, mre);
		mre.WaitOne(TimeSpan.FromSeconds(30));
		Console.WriteLine("done");
	}	
}

// Define other methods and classes here
public void Spin(int seconds, ManualResetEvent hnd) 
{
	Console.WriteLine("spinning");
	Thread.Sleep(seconds * 1000);
	hnd.Set();
	Console.WriteLine("set");
}
