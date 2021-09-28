﻿using SFML.System;
using SFML.Graphics;

namespace Pacman
{
    public class Entity
    {
        private string textureName = "";
        protected Sprite sprite;
        public bool Dead;

        public Entity(string textureName)
        {
            this.textureName = textureName;
            sprite = new Sprite();
        }

        public Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }

        public virtual FloatRect Bounds
        {
            get;
            set;
        }

        public virtual bool Solid { get; }


        public virtual void Create(Scene scene)
        {
            sprite.Texture = scene.Assets.LoadTexture(textureName);
        }

        public virtual void Destroy(Scene scene)
        {
            
        }

        public virtual void Update(Scene scene, float deltaTime)
        {
            foreach (Entity found in scene.FindIntersects(Bounds))
            {
                CollideWith(scene, found);
            }
        }

        public void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }

        protected virtual void CollideWith(Scene scene, Entity other)
        {
            
        }
    }
}