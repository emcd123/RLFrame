using Microsoft.Xna.Framework;

using SadConsole;
using System;
using Console = SadConsole.Console;

namespace ConsoleLayers
{
    class TitleConsole : Console
    {
        public TitleConsole(string title) : base(25, 6)
        {
            Fill(Color.White, Color.Black, 176);
            Print(0, 0, title.Align(HorizontalAlignment.Center, Width), Color.Black, Color.Yellow);
        }
    }
}