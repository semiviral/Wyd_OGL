#region

using System;
using System.Diagnostics;
using Wyd.Engine;
using Wyd.Engine.Math;

#endregion

namespace Wyd
{
    internal static class Program
    {
        private static ConsoleManager _consoleManager;
        private static Game _game;

        private static void Main(string[] args)
        {
            _consoleManager = new ConsoleManager();
            ConsoleManager.Hide(false);
            
            _game = new Game("Test Game Engine", true, new int2(800, 600));
            _game.Run();
            
        }
    }
}
