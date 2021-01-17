<Query Kind="Statements" />

const int limit = 5;
var rng = new Queue<int>(limit);

for (var i=0; i < 50; i++) {
	rng.Enqueue(i);
	if (rng.Count > limit) {
		rng.Dequeue();
	}
	$"[{rng.Count}]{String.Join(",", rng)}".Dump();
}
