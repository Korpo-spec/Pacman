using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Pacman
{
    public class Pacman : Actor
    {
        public override void Create(Scene scene)
        {
            speed = 100f;
            originalPosition = Position;

            base.Create(scene);
            sprite.TextureRect = new IntRect(0, 0, 18, 18);

            scene.Events.LoseHealth += OnLoseHealth;
        }

        private float timer = 0;
        private bool firstFrame;

        public override void Update(Scene scene, float deltaTime)
        {
            timer += deltaTime;
            if (timer > 1 / 10f)
            {
                firstFrame = !firstFrame;
                timer = 0;
            }

            base.Update(scene, deltaTime);
        }

        public override FloatRect Bounds
        {
            get
            {
                var bounds = base.Bounds;
                bounds.Left += 3;
                bounds.Width -= 6;
                bounds.Top += 3;
                bounds.Height -= 6;
                return bounds;
            }
        }

        public override void Render(RenderTarget target)
        {
            switch (direction)
            {
                case 0:
                    sprite.TextureRect = new IntRect(firstFrame ? 0 : 18, 0, 18, 18);
                    break;
                case 1:
                    sprite.TextureRect = new IntRect(firstFrame ? 0 : 18, 18, 18, 18);
                    break;
                case 2:
                    sprite.TextureRect = new IntRect(firstFrame ? 0 : 18, 36, 18, 18);
                    break;
                case 3:
                    sprite.TextureRect = new IntRect(firstFrame ? 0 : 18, 54, 18, 18);
                    break;
            }

            base.Render(target);
        }

        public override void Destroy(Scene scene)
        {
            base.Destroy(scene);
            scene.Events.LoseHealth -= OnLoseHealth;
        }

        private void OnLoseHealth(Scene scene, int amount)
        {
            Reset();
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