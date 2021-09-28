﻿using System;
using System.Linq;
using SFML.Graphics;
using SFML.System;

namespace Pacman
{
    public class Actor : Entity
    {
        private bool wasAligned;
        protected float speed;
        protected int direction;
        protected bool moving;
        protected Vector2f originalPosition;
        protected float originalSpeed;

        protected Actor() : base("pacman")
        {

        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            originalPosition = Position;
            originalSpeed = speed;
        }

        protected bool IsAlligned => 
            (int) MathF.Floor(Position.X) % 18 == 0 && 
            (int) MathF.Floor(Position.Y) % 18 == 0;

        protected bool IsFree(Scene scene, int dir)
        {
            Vector2f at = Position + new Vector2f(9, 9);
            at += 18 * ToVector(dir);
            FloatRect rect = new FloatRect(at.X, at.Y, 1, 1);
            return !scene.FindIntersects(rect).Any(e => e.Solid);
        }

        protected static Vector2f ToVector(int dir)
        {
            switch (dir)
            {
                case 0:
                    return new Vector2f(1, 0); // right
                case 1:
                    return new Vector2f(0, -1); // up
                case 2:
                    return new Vector2f(-1, 0); // left
                case 3:
                    return new Vector2f(0, 1); // down
                
            }
        }

        protected virtual int PickDirection(Scene scene)
        {
            return 0;
        }
    }

}