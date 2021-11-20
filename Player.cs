using System;
using System.Numerics;
using Raylib_cs;

namespace Dialogue_Game
{
    class Player
    {
        Vector2 pos = new Vector2();
        Vector2 vel = new Vector2();

        enum Directions
        {
            Forward, // Facing Camera
            Backward, // Away Camera
            Right,
            Left
        }
        Directions currentDir = Directions.Forward;

        const int EYE_LENGTH_DOWN = 15;
        const int EYE_SIZE = 8;

        const int INTERACT_SIZE = 80;
        const float INTERACT_THRESHOLD = 0.5f;

        const int WIDTH = 30;
        const int HEIGHT = 70;
        const int SPEED = 8;

        const int CAMERA_OFFSET_SIZE_TRIGGER = 15;

        Color color = Color.BLUE;

        bool movement = true;
        Interactable currentInteraction;

        public Player(int x, int y)
        {
            pos.X = x;
            pos.Y = y;
        }

        public void Update()
        {
            checkKeyInput();
            if (movement)
            {
                updateDir();
                pos += vel;
            }
            pos -= Program.CameraOffset;
            checkObstacle();
        }

        public void Display()
        {
            Raylib.DrawRectangle((int)pos.X, (int)pos.Y, WIDTH, HEIGHT, color);

            int x1;
            int x2;
            switch (currentDir)
            {
                case Directions.Right:
                    x1 = (int)(pos.X + (WIDTH * 3f / 4f));
                    Raylib.DrawRectangle(x1 - EYE_SIZE / 2, (int)pos.Y + EYE_LENGTH_DOWN - EYE_SIZE / 2, EYE_SIZE, EYE_SIZE, Color.DARKBROWN);
                    break;

                case Directions.Left:
                    x1 = (int)(pos.X + (WIDTH * 1f / 4f));
                    Raylib.DrawRectangle(x1 - EYE_SIZE / 2, (int)pos.Y + EYE_LENGTH_DOWN - EYE_SIZE / 2, EYE_SIZE, EYE_SIZE, Color.DARKBROWN);
                    break;

                case Directions.Forward:
                    x1 = (int)(pos.X + (WIDTH * 1f / 4f));
                    x2 = (int)(pos.X + (WIDTH * 3f / 4f));
                    Raylib.DrawRectangle(x1 - EYE_SIZE / 2, (int)pos.Y + EYE_LENGTH_DOWN - EYE_SIZE / 2, EYE_SIZE, EYE_SIZE, Color.DARKBROWN);
                    Raylib.DrawRectangle(x2 - EYE_SIZE / 2, (int)pos.Y + EYE_LENGTH_DOWN - EYE_SIZE / 2, EYE_SIZE, EYE_SIZE, Color.DARKBROWN);
                    break;

                case Directions.Backward:
                    break;
            }

            //Raylib.DrawCircleLines((int)pos.X + WIDTH / 2, (int)pos.Y + HEIGHT / 2, INTERACT_SIZE, Color.WHITE);

            if (!movement)
            {
                currentInteraction.DisplayFlavorText();
            }
        }

        private void checkKeyInput()
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_UP)) vel.Y = -SPEED;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) vel.X = -SPEED;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) vel.X = SPEED;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)) vel.Y = SPEED;

            if (Raylib.IsKeyReleased(KeyboardKey.KEY_W) || Raylib.IsKeyReleased(KeyboardKey.KEY_UP)) vel.Y = 0;
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_S) || Raylib.IsKeyReleased(KeyboardKey.KEY_DOWN)) vel.Y = 0;
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_A) || Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT)) vel.X = 0;
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_D) || Raylib.IsKeyReleased(KeyboardKey.KEY_RIGHT)) vel.X = 0;

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_Z))
            {
                if (movement)
                {
                    checkInteractable();
                }
                else
                {
                    nextInteraction();
                }
            }
        }

        private void updateDir()
        {
            if (vel.X > 0) currentDir = Directions.Right;
            else if (vel.X < 0) currentDir = Directions.Left;
            else if (vel.Y > 0) currentDir = Directions.Forward;
            else if (vel.Y < 0) currentDir = Directions.Backward;
        }

        private void checkObstacle()
        {
            Rectangle playerRec = new Rectangle(pos.X, pos.Y, WIDTH, HEIGHT);
            if (Program.mainLevel.CheckObstacleCollision(playerRec))
            {
                pos -= vel;
                vel *= 0;
            }
        }

        private void checkInteractable()
        {
            Vector2 dir = getDirVector();
            Vector2 centerPos = pos + new Vector2(WIDTH / 2, HEIGHT / 2);

            Interactable potentialInteraction = Program.mainLevel.checkAndGiveInteractionCollision(centerPos, dir, INTERACT_SIZE, INTERACT_THRESHOLD);
            if (potentialInteraction != null)
            {
                movement = false;
                currentInteraction = potentialInteraction;
                potentialInteraction.RestartFlavourIndex();
            }
        }

        private Vector2 getDirVector()
        {
            switch (currentDir)
            {
                case Directions.Right:
                    return new Vector2(1, 0);
                case Directions.Left:
                    return new Vector2(-1, 0);
                case Directions.Forward:
                    return new Vector2(0, 1);
                case Directions.Backward:
                    return new Vector2(0, -1);
                default:
                    return new Vector2();
            }
        }

        private void nextInteraction()
        {
            if (!currentInteraction.NextFlavorText())
            {
                movement = true;
                currentInteraction = null;
            }
        }

        public Vector2 GetCameraOffset()
        {
            Vector2 offset = pos - new Vector2(Program.WIDTH / 2, Program.HEIGHT / 2);
            if (offset.Length() < CAMERA_OFFSET_SIZE_TRIGGER) return Vector2.Zero;
            Vector2 CameraLimit = Vector2.Normalize(offset) * CAMERA_OFFSET_SIZE_TRIGGER;
            offset = offset - CameraLimit;
            return offset;
        }
    }
}
