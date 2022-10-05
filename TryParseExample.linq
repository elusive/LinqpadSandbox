<Query Kind="Statements" />

// some values for parsing attempt
// need to be able to take in object
// as well as string value to parse

object value = null; // "true", "false", "" 
bool parsed = false;


// this stmt will return false for non-truthy values such as
// empty string and null and only true for string "[T/t]rue"
parsed = Boolean.TryParse(value?.ToString(), out bool result);

value.Dump($"value:{value?.GetType()}");
parsed.Dump("parsed");
result.Dump("result");

