﻿using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace Pacman
{
    
    
    public sealed class Scene
    {
        private List<Entity> entities;
        public readonly SceneLoader Loader;
        public readonly Assetmanager Assets;
        public readonly EventManager Events;

        public Scene()
        {
            entities = new List<Entity>();
            Loader = new SceneLoader();
            Assets = new Assetmanager();
            Events = new EventManager();
        }

        public void Spawn(Entity entity)//Spawn and run create func
        {
            entities.Add(entity);
            entity.Create(this);
        }

        public void Clear() //Clear the whole entities list and play entities destroy function
        {
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                if (!entity.DontDestroyOnLoad)
                {
                    entities.RemoveAt(i);
                    entity.Destroy(this);
                }
            }
        }

        public void UpdateAll(float deltaTime)//Updates, runs events and then removes entities
        {
            Loader.HandleSceneLoad(this);

            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                entity.Update(this, deltaTime);
            }

            Events.HandleEvents(this);

            for (int i = 0; i < entities.Count;)
            {
                Entity entity = entities[i];
                if (entity.Dead)
                    entities.RemoveAt(i);
                else
                    i++;
            }
        }

        public void RenderAll(RenderTarget target)//Renders all entities
        {
            foreach (var entity in entities)
            {
                entity.Render(target);
            }
        }

        public IEnumerable<Entity> FindIntersects(FloatRect bounds)//Find all intersects between chosen bounds and all other entities
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

        public bool FindByType<T>(out T found) where T : Entity //Find an entitie by type where type is a type of Entity
        {
            foreach (Entity entity in entities)
            {
                if (entity is T typed && !entity.Dead)
                {
                    found = typed;
                    return true;
                }
            }

            found = default(T);
            return false;
        }
    }
}