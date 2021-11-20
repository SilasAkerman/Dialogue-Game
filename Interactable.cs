using System;
using System.Numerics;
using Raylib_cs;

namespace Dialogue_Game
{
    class Interactable : Obstacle
    {
        private Vector2 _interactPoint = new Vector2();
        public Vector2 InteractPoint { get { return _interactPoint + pos; } set { _interactPoint = value + new Vector2(width / 2, height / 2); } }

        string[] flavorText;
        int flavorTextIndex = 0;

        public Interactable(int x, int y, int aWidth, int aHeight, string[] aFlavorText) : base(x, y, aWidth, aHeight)
        {
            color = Color.YELLOW;
            _interactPoint += new Vector2(width / 2, height / 2);
            flavorText = aFlavorText;
        }

        public override void Display()
        {
            base.Display();
        }

        public void DisplayFlavorText()
        {
            Rectangle textScreen = new Rectangle(50, Program.HEIGHT - 250, Program.WIDTH - 50*2, 200);
            Raylib.DrawRectangleRounded(textScreen, 0.2f, 10, Color.BLACK);
            Raylib.DrawRectangleRoundedLines(textScreen, 0.2f, 10, 2, Color.WHITE);
            textScreen.x += 20;
            textScreen.y += 20;
            textScreen.width -= 20 * 2;
            textScreen.height -= 20 * 2;
            Raylib.DrawTextRec(Raylib.GetFontDefault(), flavorText[flavorTextIndex], textScreen, 36, 10, true, Color.WHITE);
        }

        public bool NextFlavorText()
        {
            flavorTextIndex++;
            if (flavorTextIndex >= flavorText.Length) return false;
            return true;
        }

        public string GetCurrentFlavortext()
        {
            return flavorText[flavorTextIndex];
        }

        public void DisplayInteractPoint()
        {
            Raylib.DrawCircleV(InteractPoint, 5, Color.RED);
        }

        public void RestartFlavourIndex()
        {
            flavorTextIndex = 0;
        }
    }
}
