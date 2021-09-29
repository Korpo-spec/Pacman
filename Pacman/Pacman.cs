﻿using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;
namespace Pacman
{
    public class Pacman : Actor
    {
        public override void Create(Scene scene)
        {
            speed = 100f;
            sprite.TextureRect = new IntRect(0,0,18,18);
            originalPosition = Position;

            base.Create(scene);
            scene.LoseHealth += OnLoseHealth;
            
        }

        public override void Destroy(Scene scene)
        {
            
            base.Destroy(scene);
            scene.LoseHealth -= OnLoseHealth;
        }

        private void OnLoseHealth(Scene scene, int amount)
        {
            Position = originalPosition;
        }

        protected override int PickDirection(Scene scene)
        {
            int dir = direction;
            if (Keyboard.IsKeyPressed(Right))
            {
                dir = 0;
                moving = true;
            }
            else if (Keyboard.IsKeyPressed(Up))
            {
                dir = 1;
                moving = true;
            }
            else if (Keyboard.IsKeyPressed(Left))
            {
                dir = 2;
                moving = true;
            }
            else if (Keyboard.IsKeyPressed(Down))
            {
                dir = 3;
                moving = true;
            }

            if (IsFree(scene, dir)) return dir;
            if (!IsFree(scene, direction)) moving = false;
            return direction;
        }
    }
}