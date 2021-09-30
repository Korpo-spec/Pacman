using SFML.Graphics;

namespace Pacman
{
    public class Coin : Entity
    {
        public Coin() : base("pacman")
        {
            
        }
        public override void Create(Scene scene)
        {
            sprite.TextureRect = new IntRect(36, 36, 18, 18);
            base.Create(scene);
        }
        
        protected override void CollideWith(Scene scene, Entity other)
        {
            if (other is Pacman)
            {
                Dead = true;
                scene.Events.PublishGainedScore(100);

            }
            
        }
    }
}