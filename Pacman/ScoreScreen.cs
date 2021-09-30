using System;
using SFML.Graphics;
using SFML.System;
using System.IO;
using SFML.Window;

namespace Pacman
{
    public class ScoreScreen: Entity
    {
        public ScoreScreen() : base("pacman")
        {
            scoreText = new Text();
        }

        private Text scoreText;
        public override void Create(Scene scene)
        {
            string fileString = File.ReadAllText("highscore.txt");
            scoreText.Font = scene.Assets.LoadFont("pixel-font");
            scoreText.DisplayedString = $"highscore: {fileString}";
            scoreText.FillColor = Color.Black;
            DontDestroyOnLoad = false;
            base.Create(scene);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space)) scene.Loader.Load("maze") ;
        }

        public override void Render(RenderTarget target)
        {
            string fileString = File.ReadAllText("highscore.txt");
            scoreText.DisplayedString = $"highscore: {fileString}";
            scoreText.Position = new Vector2f(scoreText.GetGlobalBounds().Width/4, scoreText.GetGlobalBounds().Height/2);

            target.Draw(scoreText);
        }
    }
}