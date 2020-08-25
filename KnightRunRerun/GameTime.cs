using System;

namespace KnightRunRerun
{
    public struct GameTime
    {
        /// <summary>
        /// Time elapsed since the previous frame.
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// Time elapsed since the start of the program.
        /// </summary>
        public TimeSpan TotalTime { get; set; }
    }
}
