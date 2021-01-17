<Query Kind="Program" />

void Main()
{
	var numbers = new[] { 1, 1, 3, 2, 3, 2, 3, 1, 4, 5 };
	
	var counts = frequency(numbers);
	counts.Dump();
	counts.Max(c => c[1]).Dump();
	var ans = counts.Where(c => c[1] == counts.Max(c => c[1])).Min(c => c[0]);
	ans.Dump();
}
	

/*
 * 	Hacker Rank - Problem Solving Practice
 * 	Migratory Birds https://www.hackerrank.com/challenges/migratory-birds/problem
 */

static IEnumerable<int[]> frequency(int[] numbers)
{
	return  numbers.OrderBy(n => n).Distinct().Select(n =>
		new int[] {
			n,
			numbers.Where(x => x == n).Count()
		}

	);
}