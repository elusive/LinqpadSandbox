<Query Kind="Statements" />

var points = new List<System.Drawing.Point>();
for (var cp = 2; cp <= 5; cp++) {
	for (var sp = 1; sp <= 4; sp++) {
		if (cp == 3 && sp > 2) continue;	
		points.Add(new System.Drawing.Point(cp, sp));
	}
}

var sorted = points.OrderByDescending(p => p.Y)
                        .GroupBy(p => p.X)
						.Select(g => g.First())
						.OrderBy(p => p.Y);


sorted.Dump();