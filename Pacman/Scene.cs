using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace Pacman
{
    public delegate void ValueChangedEvent(Scene scene, int value);
    
    public sealed class Scene
    {
        public event ValueChangedEvent GainScore;
        public event ValueChangedEvent LoseHealth;
        
        private List<Entity> entities;
        public readonly SceneLoader Loader;
        public readonly Assetmanager Assets;

        public Scene()
        {
            entities = new List<Entity>();
            Loader = new SceneLoader();
            Assets = new Assetmanager();
        }

        private int scoreGained;
        private int healthLost;
        public void PublishGainedScore(int amount) => scoreGained += amount;
        public void PublishLoseHealth(int amount) => healthLost += amount;

        public void Spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }

        public void Clear()
        {
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }

        public void UpdateAll(float deltaTime)
        {
            
            
            Loader.HandleSceneLoad(this);

            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                entity.Update(this, deltaTime);

            }

            if (scoreGained != 0)
            {
                GainScore?.Invoke(this,scoreGained);
                scoreGained = 0;
            }

            if (healthLost != 0)
            {
                LoseHealth?.Invoke(this,healthLost);
                healthLost = 0;
            }
            for (int i = 0; i < entities.Count;)
            {
                Entity entity = entities[i];
                if (entity.Dead) entities.RemoveAt(i);
                else i++;
            }
        }

        public void RenderAll(RenderTarget target)
        {
            foreach (var entity in entities)
            {
               entity.Render(target); 
            }
        }

       

        public IEnumerable<Entity> FindIntersects(FloatRect bounds)
        {
            int lastEntity = entities.Count - 1;

            for (int i = lastEntity; i >= 0; i--)
            {
                Entity entity = entities[i];
                if (entity.Dead) continue;
                if (entity.Bounds.Intersects(bounds))
                {
                    yield return entity;
                }

            }
        }
    }
}