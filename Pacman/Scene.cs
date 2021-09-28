using System.Collections.Generic;
using SFML.Graphics;

namespace Pacman
{
    public class Scene
    {
        private List<Entity> entities;
        public readonly SceneLoader Loader;
        public readonly Assetmanager Assets;

        public Scene()
        {
            entities = new List<Entity>();
            Loader = new SceneLoader();
            Assets = new Assetmanager();
        }

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