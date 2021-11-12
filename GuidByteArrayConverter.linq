<Query Kind="Statements" />

var id = System.Guid.NewGuid();
id.Dump();

var bytes = id.ToByteArray();
bytes.Dump();

var id2 = new Guid(bytes);
id2.Dump();

(id == id2).Dump("Are Equal");