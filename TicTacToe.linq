<Query Kind="Program">
  <Reference>&lt;ProgramFilesX86&gt;\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.2\WindowsBase.dll</Reference>
  <Namespace>System.Windows</Namespace>
</Query>



void Main()
{
	var brd = new Board();
	var move = CollectMove();
	brd.EnterMove(move.Item1, move.Item2);
	
	if(brd.CheckForWinner() != TicTacToeMove.Blank)
	{
		brd.CheckForWinner().Dump();
	}
	
	move.ToString().Dump();
}

// Define other methods and classes here
Tuple<System.Windows.Point?, TicTacToeMove> CollectMove() 
{
	Point? move = null;
	TicTacToeMove piece = TicTacToeMove.Blank;
	
	do
	{
		try {
			WL("Enter your move (Row,Col X/O):\n");
			var input = RL().Split(' ');
			move = Point.Parse(input[0]);
			piece = (TicTacToeMove)Enum.Parse(typeof(TicTacToeMove), input[1]);
			return new Tuple<Point?, TicTacToeMove>(move, piece);
		}
		catch(System.InvalidOperationException ioex)
		{
			WL(ioex.Message);
			WL("Please enter using format: \"x,y X/O\"");
			WL("");
		}
		catch(Exception ex)
		{
			WL(ex.Message);
			WL("");
			move = null;
		}
	} while(move == null);
	
	// should never get here
	return null;
}

string RL()
{
	return Console.ReadLine();
}

void WL(string value)
{
	Console.WriteLine(value);
}

class Board
{
	// array of points on the board
	static Point[] BoardPoints = new[] 
	{
		new Point(1, 1),
		new Point(1, 2),
		new Point(1, 3),
		new Point(2, 1),
		new Point(2, 2),
		new Point(2, 3),
		new Point(3, 1),
		new Point(3, 2),
		new Point(3, 3)
	};
	
	// dictionary of board points and moves
	Dictionary<Point?, TicTacToeMove> GameBoard = 
		new Dictionary<Point?, TicTacToeMove> 
		{
			{ BoardPoints[0], TicTacToeMove.Blank },
			{ BoardPoints[1], TicTacToeMove.Blank },
			{ BoardPoints[2], TicTacToeMove.Blank },
			{ BoardPoints[3], TicTacToeMove.Blank },
			{ BoardPoints[4], TicTacToeMove.Blank },
			{ BoardPoints[5], TicTacToeMove.Blank },
			{ BoardPoints[6], TicTacToeMove.Blank },
			{ BoardPoints[7], TicTacToeMove.Blank },
			{ BoardPoints[8], TicTacToeMove.Blank }		
		};
	
	// enters a new move onto the board
	public void EnterMove(Point? p, TicTacToeMove move)
	{
		if (!GameBoard.ContainsKey(p))
		{
			throw new System.ApplicationException("Invalid board position!");
		}
		
		if (GameBoard[p] != TicTacToeMove.Blank)
		{
			throw new System.ApplicationException("Position is already filled!");
		}
		
		GameBoard[p] = move;				
	}
	
	public TicTacToeMove CheckForWinner() 
	{
		// top row
		if (GameBoard[BoardPoints[0]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[1]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[2]] == TicTacToeMove.X)
			return TicTacToeMove.X;
		if (GameBoard[BoardPoints[0]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[1]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[2]] == TicTacToeMove.O)
			return TicTacToeMove.O;
		
		// middle row
		if (GameBoard[BoardPoints[3]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[4]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[5]] == TicTacToeMove.X)
			return TicTacToeMove.X;
		if (GameBoard[BoardPoints[3]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[4]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[5]] == TicTacToeMove.O)
			return TicTacToeMove.O;
			
		// bottom row
		if (GameBoard[BoardPoints[6]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[7]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[8]] == TicTacToeMove.X)
			return TicTacToeMove.X;
		if (GameBoard[BoardPoints[6]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[7]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[8]] == TicTacToeMove.O)
			return TicTacToeMove.O;
			
		// first col
		if (GameBoard[BoardPoints[0]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[3]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[6]] == TicTacToeMove.X)
			return TicTacToeMove.X;
		if (GameBoard[BoardPoints[0]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[3]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[6]] == TicTacToeMove.O)
			return TicTacToeMove.O;
			
		// second col
		if (GameBoard[BoardPoints[1]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[4]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[7]] == TicTacToeMove.X)
			return TicTacToeMove.X;
		if (GameBoard[BoardPoints[1]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[4]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[7]] == TicTacToeMove.O)
			return TicTacToeMove.O;
			
		// third col
		if (GameBoard[BoardPoints[2]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[5]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[8]] == TicTacToeMove.X)
			return TicTacToeMove.X;
		if (GameBoard[BoardPoints[2]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[5]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[8]] == TicTacToeMove.O)
			return TicTacToeMove.O;
			
		// 1 to 9 diagonal
		if (GameBoard[BoardPoints[0]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[4]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[8]] == TicTacToeMove.X)
			return TicTacToeMove.X;
		if (GameBoard[BoardPoints[0]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[4]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[8]] == TicTacToeMove.O)
			return TicTacToeMove.O;
			
		// 7 to 3 diagonal
		if (GameBoard[BoardPoints[6]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[4]] == TicTacToeMove.X &&
			GameBoard[BoardPoints[2]] == TicTacToeMove.X)
			return TicTacToeMove.X;
		if (GameBoard[BoardPoints[6]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[4]] == TicTacToeMove.O &&
			GameBoard[BoardPoints[2]] == TicTacToeMove.O)
			return TicTacToeMove.O;
			
		// no winner yet
		return TicTacToeMove.Blank;
	}
}

enum TicTacToeMove
{
	X,
	O,
	Blank
}

