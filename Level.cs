using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Dialogue_Game
{
    class Level
    {
        List<Obstacle> obstacles = new List<Obstacle>();
        List<Interactable> interactables = new List<Interactable>();
        List<Obstacle> totalThings = new List<Obstacle>();

        public void Update()
        {
            foreach (Obstacle thing in totalThings)
            {
                thing.CameraOffset(Program.CameraOffset);
            }
        }

        public void Display()
        {
            foreach (Obstacle thing in totalThings)
            {
                thing.Display();
            }

            //foreach (Interactable interactable in interactables)
            //{
            //    interactable.DisplayInteractPoint();
            //}
        }

        public void AddInteractable(Interactable interactable, bool obstacle=false)
        {
            interactables.Add(interactable);
            if (obstacle) obstacles.Add(interactable);
            totalThings.Add(interactable);
        }

        public void AddObstacle(Obstacle obstacle)
        {
            obstacles.Add(obstacle);
            totalThings.Add(obstacle);
        }

        public Interactable checkAndGiveInteractionCollision(Vector2 playerPos, Vector2 playerDir, int interactSize, float interactTrigger) 
        {
            foreach (Interactable potentialInteraction in interactables)
            {
                if (Vector2.Distance(playerPos, potentialInteraction.InteractPoint) < interactSize)
                {
                    Vector2 interactDir = Vector2.Normalize(potentialInteraction.InteractPoint - playerPos);
                    playerDir = Vector2.Normalize(playerDir);
                    if (Vector2.Dot(playerDir, interactDir) > interactTrigger) return potentialInteraction;
                }
            }

            return null;
        }

        public bool CheckObstacleCollision(Rectangle playerRec)
        {
            foreach (Obstacle obstacle in obstacles)
            {
                if (Raylib.CheckCollisionRecs(playerRec, obstacle.GetRectangle()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
