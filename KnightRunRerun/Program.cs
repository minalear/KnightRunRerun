using System;

namespace KnightRunRerun
{
    // Entry to our game
    class Program
    {
        public static void Main(string[] args)
        {
            // using will call dispose automatically when the game closes
            using (var game = new Game(1280, 720, "Knight Run the Rerun"))
            {
                game.Run();
            }
        }
    }
}
