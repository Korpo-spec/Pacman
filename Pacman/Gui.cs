using System;
using SFML.Graphics;
using SFML.System;

namespace Pacman

{
    public class Gui : Entity
    {
        private Text scoreText;
        private int maxHealth = 4;
        private int currentHealth;
        private int currentScore;

        public Gui() : base("pacman")
        {
            scoreText = new Text();
        }
        public override void Create(Scene scene)
        {
            sprite.TextureRect = new IntRect(72, 36, 18, 18);
            scoreText.Font = scene.Assets.LoadFont("pixel-font");
            scoreText.DisplayedString = $"Score: {currentScore}";
            scoreText.FillColor = Color.Black;
            currentHealth = maxHealth;
            scene.Events.LoseHealth += OnLoseHealth;
            scene.Events.GainScore += OnGainScore;
            base.Create(scene);
        }

        public override void Render(RenderTarget target)
        {
            sprite.Position = new Vector2f(36, 396);
            for (int i = 0; i < maxHealth; i++)
            {
                sprite.TextureRect = i < currentHealth 
                    ? new IntRect(72, 36, 18, 18) 
                    : new IntRect(72, 0, 18, 18);
                base.Render(target);
                sprite.Position += new Vector2f(18, 0);
            }
            scoreText.DisplayedString = $"Score: {currentScore}";

            scoreText.Position = new Vector2f(414 - scoreText.GetGlobalBounds().Width, 396);
            target.Draw(scoreText);
        }

        private void OnLoseHealth(Scene scene, int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                DontDestroyOnLoad = false;
                scene.Loader.Reload();
            }
        }

        private void OnGainScore(Scene scene, int amount)
        {
            
            currentScore += amount;
            if (!scene.FindByType<Coin>(out _))
            {
                
                DontDestroyOnLoad = true;
                scene.Loader.Reload();
            }
            

        }
        
        
    }
}