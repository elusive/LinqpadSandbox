<Query Kind="FSharpProgram" />

let inc x = x + 1;
let square x = x * x;
let toS x = x.ToString()
let squareThenIncrementThenToString = square >> inc >> toS
(squareThenIncrementThenToString 2).Dump()

