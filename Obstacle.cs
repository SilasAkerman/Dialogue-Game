using System;
using System.Numerics;
using Raylib_cs;

namespace Dialogue_Game
{
    class Obstacle
    {
        protected Vector2 pos = new Vector2();
        protected int width;
        protected int height;

        protected Color color = Color.WHITE;

        public Obstacle(int x, int y, int aWidth, int aHeight)
        {
            pos.X = x;
            pos.Y = y;
            width = aWidth;
            height = aHeight;
        }

        public virtual void Display()
        {
            Raylib.DrawRectangle((int)pos.X, (int)pos.Y, width, height, color);
        }

        public virtual void CameraOffset(Vector2 offset)
        {
            pos -= offset;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, width, height);
        }
    }
}
