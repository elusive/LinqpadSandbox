<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static readonly object _locker = new object();

void Main()
{
	const long EXPECTED_SUM = 500500;
	var src = Enumerable.Range(1, 1000);
	
	// no synchronization
	long sum = 0;
	Parallel.ForEach(src, n =>
	{
		sum += n;
	});
	sum.Dump("Sum w/o Synchronization");    // should be 500500 some of the time
	Debug.Assert(sum == EXPECTED_SUM, "Sum equals expected value.");


	// sync with lock
	long sum2 = 0;
	Parallel.ForEach(src, n => 
	{
		lock(_locker) { sum2 += n; }
	});
	sum2.Dump("Sum synchronzied with lock");    // will be 500500
	Debug.Assert(sum2 == EXPECTED_SUM, "Sum equals expected value.");


	// sync with Interlocked class
	long sum3 = 0;
	Parallel.ForEach(src, n => 
	{
		Interlocked.Add(ref sum3, n);
	});
	sum3.Dump("Sum synchronized with Interlocked.Add");	// 500500
	Debug.Assert(sum3 == EXPECTED_SUM, "Sum equals expected value.");
}
