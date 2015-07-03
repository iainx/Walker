using System;

namespace Walker.Map
{
    [Flags]
    public enum Direction {
        None = 0,
        North = 1, // 0, -1
        South = 2, // 0, 1
        East = 3,  // -1, 0
        West = 4   // 1, 0
    };
}

