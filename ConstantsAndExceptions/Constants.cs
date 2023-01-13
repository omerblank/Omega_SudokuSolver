using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Constants
{
    public const int MIN_SIZE = 1;//--> 1^2=1 --> 1*1
    public const int MAX_SIZE = 5;//--> 5^2=25 --> 25*25
    public const int NO_VALUE = 0;
    public const string BOX_COL_SEPERATOR = " | ";
    public const string BOX_ROW_SEPERATOR = "-";
    public const string ELEMENTS_SEPERATOR = " ";
    public static readonly List<string> INPUT_OPTIONS = new List<string>() { "string", "text file" };
    public const string STRING_GRID_EXAMPLE = "800000070006010053040600000000080400003000700020005038000000800004050061900002000";
}
