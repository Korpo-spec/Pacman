using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using SFML.System;

namespace Pacman
{
    public class SceneLoader
    {
        private readonly Dictionary<char, Func<Entity>> loaders;
        private string currentScene = "", nextScene = "";

        public SceneLoader()
        {
            loaders = new Dictionary<char, Func<Entity>>()
            {
                {'#', () => new Wall()}
            };
        }

        private bool Create(char symbol, out Entity created)
        {
            if (loaders.TryGetValue(symbol, out Func<Entity> loader))
            {
                created = loader();
                
                return true;
            }

            created = null;
            return false;
        }

        public void HandleSceneLoad(Scene scene)
        {
            if (nextScene == "") return;
            scene.Clear();
            string file = $"assets/{nextScene}.txt";
            string[] board = File.ReadAllLines(file, Encoding.UTF8);

            for (int i = 0; i < board.Length; i++)
            {
                char[] row = board[i].ToCharArray();
                for (int j = 0; j < row.Length; j++)
                {
                    Entity created;
                    if(!Create(row[j], out created)) continue;
                    
                    
                    created.Position = new Vector2f((i + 1) * 18, (j + 1) * 18);

                }
            }
            
            currentScene = nextScene;
            nextScene = "";
        }

        public void Load(string scene) => nextScene = scene;
        public void Reload() => nextScene = currentScene;
    }
}