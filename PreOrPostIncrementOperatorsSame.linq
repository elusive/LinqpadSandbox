<Query Kind="Statements" />


int preInc = 0;
int postInc = 0;

for (var i = 1; i <= 20; i++) {
	++preInc;
	postInc++;
	$"Loop {i}: pre = {preInc}, post = {postInc}".Dump();
}