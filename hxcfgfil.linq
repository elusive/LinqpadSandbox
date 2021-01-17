<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.VisualBasic.dll</Reference>
  <Namespace>Microsoft.VisualBasic</Namespace>
  <Namespace>Microsoft.VisualBasic.CompilerServices</Namespace>
</Query>

void Main() {
var cmdline = "../Methods/N500Method.lay 1";
string path = "";
bool binary = false;

Parse(ref cmdline, ref path, ref binary);

cmdline.Dump();
path.Dump();
binary.Dump();
}

public void Parse(ref string cmdLine, ref string pathFileName, ref bool toBinary)
    {
      int num1 = 0;
      string Right = "";
      pathFileName = "";
      toBinary = false;
      short num2 = checked ((short) Strings.Len(cmdLine));
      int num3 = 1;
      short num4 = num2;
      for (short index = (short) num3; (int) index <= (int) num4; ++index)
      {
        Right += Strings.Mid(cmdLine, (int) index, 1);
        string Left = Right;
        if (Operators.CompareString(Left, "/b", false) == 0)
        {
          if (0 == num1)
          {
            toBinary = true;
            num1 = 1;
            Right = "";
          }
        }
        else if (Operators.CompareString(Left, "/t", false) == 0)
        {
          if (0 == num1)
          {
            toBinary = false;
            num1 = 3;
            Right = "";
          }
        }
        else if (Operators.CompareString(Left, "\"", false) == 0)
        {
          switch (num1)
          {
            case 0:
              Right = "";
              continue;
            case 1:
              num1 = 2;
              Right = "";
              continue;
            case 2:
              num1 = 0;
              Right = "";
              continue;
            case 3:
              num1 = 4;
              Right = "";
              continue;
            case 4:
              num1 = 0;
              Right = "";
              continue;
            default:
              continue;
          }
        }
        else if (Operators.CompareString(Left, " ", false) == 0)
        {
          switch (num1)
          {
            case 0:
              Right = "";
              continue;
            case 1:
              Right = "";
              continue;
            case 2:
              pathFileName = pathFileName + Right;
              Right = "";
              continue;
            case 3:
              Right = "";
              continue;
            case 4:
              pathFileName = pathFileName + Right;
              Right = "";
              continue;
            default:
              continue;
          }
        }
        else
        {
          switch (num1)
          {
            case 0:
              if (Operators.CompareString("/", Right, false) != 0)
              {
                Right = "";
                continue;
              }
              continue;
            case 1:
              pathFileName = pathFileName + Right;
              Right = "";
              continue;
            case 2:
              pathFileName = pathFileName + Right;
              Right = "";
              continue;
            case 3:
              pathFileName = pathFileName + Right;
              Right = "";
              continue;
            case 4:
              pathFileName = pathFileName + Right;
              Right = "";
              continue;
            default:
              continue;
          }
        }
      }
    }
