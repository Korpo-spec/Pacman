using SFML.Graphics;

namespace Pacman
{
    public class Candy : Entity
    {
        public Candy() : base("pacman")
        {
            
        }
        public override void Create(Scene scene)
        {
            sprite.TextureRect = new IntRect(72, 54, 18, 18);
            base.Create(scene);
        }
    }
}