using System;
using System.Numerics;
using Raylib_cs;

namespace Dialogue_Game
{
    class Program
    {
        public const int WIDTH = 1000;
        public const int HEIGHT = 800;

        public static Level mainLevel = new Level();
        public static Player mainPlayer = new Player(WIDTH/2, HEIGHT/2);

        public static Vector2 CameraOffset { get; set; } = new Vector2();

        static void Main(string[] args)
        {
            Init();
            while (!Raylib.WindowShouldClose())
            {
                Update();
                Display();
            }
        }

        static void Init()
        {
            Raylib.InitWindow(WIDTH, HEIGHT, "Dialogue Game");
            Raylib.SetTargetFPS(30);
            mainLevel.AddObstacle(new Obstacle(50, 50, 20, 1100));
            mainLevel.AddObstacle(new Obstacle(50, 50, 900, 20));
            mainLevel.AddObstacle(new Obstacle(250, 300, 50, 50));
            mainLevel.AddObstacle(new Obstacle(50, 1150, 900, 20));
            mainLevel.AddObstacle(new Obstacle(950, 50, 20, 1120));

            string[] flavorText = new string[] 
            { 
                "Hello, I am just an anoying block.", 
                "Gosh, I cant even spell!", 
                "That because I dont have brain.",
                "Ha ha ha!",
                "Life is simple withuot brain!"
            };
            Interactable newInteractable = new Interactable(500, 200, 50, 50, flavorText);
            mainLevel.AddInteractable(newInteractable, obstacle: true);

            flavorText = new string[]
            {
                "I am another uniquely majestic painting.",
                "Even though I was created by the same wonderful artist,",
                "You really have to appreciate the next level of originality in these other shades of yellow.",
                "How wonderful!"
            };
            newInteractable = new Interactable(1000, 300, 50, 100, flavorText);
            newInteractable.InteractPoint = new Vector2(-50, 0);
            mainLevel.AddInteractable(newInteractable);

            flavorText = new string[]
            {
                "Hello...",
                "I am small and quite shy...",
                "so I've made myself this cozy corner...",
                "...",
                "Wanna join?"
            };
            newInteractable = new Interactable(900, 100, 30, 30, flavorText);
            mainLevel.AddInteractable(newInteractable, obstacle: true);

            flavorText = new string[] 
            { 
                "I am a majestic painting.", 
                "Just look at my wonderful shades of yellow!"
            };
            newInteractable = new Interactable(400, -100, 50, 100, flavorText);
            newInteractable.InteractPoint = new Vector2(0, 90);
            mainLevel.AddInteractable(newInteractable);

            flavorText = new string[]
            {
                "In order to hear the story of our ultimate rug,",
                "You must take it all in at once",
                "And when in the center",
                "You can finally hear the story and legacy of this amazing rug.",
                "...",
                "Huh!? You ask me why one has to go to the center to experience the rug and it's legacy?",
                "God was lazy, idk."
            };
            newInteractable = new Interactable(330, 620, 50, 50, flavorText);
            mainLevel.AddInteractable(newInteractable, obstacle: true);

            flavorText = new string[]
            {
                "The most interesting unique rug the universe has to offer.",
                "But due to it being the only rug in existence,",
                "It's really not that impressive of a title."
            };
            newInteractable = new Interactable(300, 700, 400, 400, flavorText);
            mainLevel.AddInteractable(newInteractable);

            flavorText = new string[]
            {
                "Have you looked at our majestic rug?",
                "It's the most interesting thing we have in our universe!",
                "Wanna know why?",
                "...",
                "(steps closer)",
                "Because it doesn't have have any physics collisions.",
                "That's all the majestic rug of the universe offers.",
                "The only thing that can be trampled and walked on.",
                "Ever.",
                "...",
                "Totally Amazing, right?"
            };
            newInteractable = new Interactable(900, 1000, 50, 50, flavorText);
            mainLevel.AddInteractable(newInteractable, obstacle: true);
        }

        static void Update()
        {
            CameraOffset = mainPlayer.GetCameraOffset();
            mainLevel.Update();
            mainPlayer.Update();
        }

        static void Display()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            mainLevel.Display();
            mainPlayer.Display();
            Raylib.EndDrawing();
        }
    }
}
