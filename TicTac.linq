<Query Kind="Program" />

void Main()
{
	var ttt = new TicTacToe();
	ttt.Draw();
}

// Define other methods and classes here
public static void Out(string s) { Console.WriteLine(s); }
public static string In() { return Console.ReadLine(); }


public class TicTacToe
{
	private string[][] _board;
		
	public TicTacToe()
	{
		_board = new string[][] {
			new string[] { "-", "-", "-" },
			new string[] { "-", "-", "-" },
			new string[] { "-", "-", "-" }
		};		
	}
	
	public void Draw()
	{
		Out(string.Join(" | ", _board[0]));
		Out("--------");
		Out(string.Join(" | ", _board[0]));
		Out("--------");
		Out(string.Join(" | ", _board[0]));
	}	
}