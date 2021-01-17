<Query Kind="Program" />

void Main()
{
	var words = new List<string>();
	printS("thisisawesome", words, new List<string>());	
	words.Dump();
}


bool isWord(string s) {
	var dict = new string[] {"this", "is", "a", "we", "so", "me", "awe", "some", "awesome"};
	return dict.Contains(s);
}


/*
void printSentences(String input, List<string> words) {
	
	var characters = input.ToCharArray();
    
	if (input == "") {
		words.Dump();
		return;
	}

    for (var i = 0; i < characters.Length; i++) 
    {
		var s = input.Substring(i);
		s.Dump();
        if (isWord(s)) 
        {
            words.Add(s);
			if (s.Length > i) {
            	printS(s.Substring(i), words);
			}
        }        
    }
}*/


void printS(String input, List<string> words, List<string> sentences,  int lastFound = 0) {
    
	char[] characters = input.ToCharArray();
    words = words ?? new List<string>();
	
    for (var i = 0; i <= characters.Length; i++) 
    {
		// end of input string?
		if (i == characters.Length) {
			sentences.Add(string.Join(" ", words));
			words = new List<string>();
			sentences.Dump();
			break;
		}
		
		// too long?  shouldn't happen but seems to
		if (lastFound + i > characters.Length) return;
        
		
		var s = input.Substring(lastFound, i);
		("Last Found = " + lastFound).Dump();
		(s + " isWord = " + isWord(s)).Dump();
        if (isWord(s)) 
        {
			lastFound = i;
            words.Add(s);
            printS(input.Substring(i), words, sentences);
        }        
    }
	
	var sentence = string.Join(" ", words);
	if (!sentences.Contains(sentence)) {
		"new sentence".Dump();
	    sentences.Add(sentence);	
		sentence.Dump();
	}
	//printS(input, words, sentences);
    
}

List<string> MakeBiggerWord(IEnumerable<string> words)
{
    var results = new List<string>();
    for (var i = 0; i < words.Count(); i++)
    {
        var tryMe = words.Skip(i).Take(words.Count() - i);
        //if (MakeBiggerWord(tryMe).ToArray())
        //{
        //    results.Add(string.Join(" ", tryMe));
        //}
    }
	return null;
}