<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>


class Program
{
	static void ThrowEx() { throw null; }
	static void Main(string[] args)
	{
		TaskScheduler.UnobservedTaskException += new EventHandler<UnobservedTaskExceptionEventArgs>(TaskScheduler_UnobservedTaskException);

		Task t3 = Task.Run(() =>
		{
			//try
			//{
				throw new Exception("t3 throw an exception");
			//}
			//catch {}
		});

		Thread.Sleep(5000);
	}
	static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
	{
		Console.WriteLine("Error");
		Console.WriteLine(e.Exception.Message);
		e.SetObserved();
	}
}