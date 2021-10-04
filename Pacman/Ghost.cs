using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace Pacman
{
    public class Ghost : Actor
    {
        private float frozenTimer;
        public override void Create(Scene scene)
        {
            direction = -1;
            speed = 100.0f;
            moving = true;
            scene.Events.CandyEaten += () => frozenTimer = 5;
            base.Create(scene);
            sprite.TextureRect = new IntRect(36, 0, 18, 18);
            originalPosition = Position;
            
        }

        protected override void CollideWith(Scene scene, Entity other)
        {
            if (other is Pacman)
            {
                if (frozenTimer <= 0)
                {
                    scene.Events.PublishLoseHealth(1);
                    
                }
                else
                {
                    scene.Events.PublishGainedScore(200);
                    
                }
                Reset();
                
            }
        }

        protected override int PickDirection(Scene scene)
        {
            List<int> validMoves = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                if ((i + 2)% 4 == direction) continue;
                if (IsFree(scene, i)) validMoves.Add(i);
                

            }

            int r = new Random().Next(0, validMoves.Count);
            return validMoves[r];
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            frozenTimer = MathF.Max(frozenTimer - deltaTime, 0.0f);
            if (frozenTimer <= 0)
            {
                speed = originalSpeed;
            }
            else
            {
                speed = 75.0f;
            }
        }

        public override void Render(RenderTarget target)
        {
            if (frozenTimer > 0.0f)
            {
                sprite.TextureRect = new IntRect(36, 18, 18, 18);
            }
            else
            {
                sprite.TextureRect = new IntRect(36, 0, 18, 18);
            }
            base.Render(target);
        }
    }
}