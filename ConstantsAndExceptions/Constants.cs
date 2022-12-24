using System;
static class Constants
{
    public const int SIDE = 9;
    public static readonly IEnumerable<int> VALID_CELL_VALUES = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
}
