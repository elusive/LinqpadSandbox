<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
	try
	{
		new Canceller().BlackBoxOperationAsync(null, CancellationToken.None, 1200).Wait();
		"Completed.".Dump();
		new Canceller().BlackBoxOperationAsync(null, CancellationToken.None, 900).Wait();
	}
	catch (Exception ex)
	{
		Exception? iex = null;
		while (ex is AggregateException)
			iex = ex.InnerException;
		iex?.Message.Dump();
	}
	
	"Done".Dump();
}

// You can define other methods, fields, classes and namespaces heres
class Canceller
{
	// .NET 4.5/C# 5.0: convert EAP pattern into TAP pattern with timeout
	public async Task<AsyncCompletedEventArgs> BlackBoxOperationAsync(
		object state,
		CancellationToken token,
		int timeout = Timeout.Infinite)
	{
		var tcs = new TaskCompletionSource<AsyncCompletedEventArgs>();
		using (var cts = CancellationTokenSource.CreateLinkedTokenSource(token))
		{
			// prepare the timeout
			if (timeout != Timeout.Infinite)
			{
				cts.CancelAfter(timeout);
			}

			// handle completion
			AsyncCompletedEventHandler handler = (sender, args) =>
			{
				if (args.Cancelled)
					tcs.TrySetCanceled();
				else if (args.Error != null)
					tcs.SetException(args.Error);
				else
					tcs.SetResult(args);
			};

			this.BlackBoxOperationCompleted += handler;
			try
			{
				using (cts.Token.Register(() => tcs.SetCanceled(), useSynchronizationContext: false))
				{
					this.StartBlackBoxOperation(null);
					return await tcs.Task.ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			finally
			{
				this.BlackBoxOperationCompleted -= handler;
			}
		}
	}

	// emulate async operation
	AsyncCompletedEventHandler BlackBoxOperationCompleted = delegate { };

	void StartBlackBoxOperation(object state)
	{
		ThreadPool.QueueUserWorkItem(s =>
		{
			Thread.Sleep(3000);
			this.BlackBoxOperationCompleted(this, new AsyncCompletedEventArgs(error: null, cancelled: false, userState: state));
		}, state);
	}
}
