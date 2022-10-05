<Query Kind="Program" />

void Main()
{
	var value1 = 100;
	var value2 = 200;
	
	var result = Interlocked.Exchange(ref _store, value1);
	
	result.Dump("result");
	_store.Dump("store");


	var compared = Interlocked.CompareExchange(ref _store, 100, 50);
	compared.Dump("compared");
	_store.Dump("store");
}


private int _store = 0;