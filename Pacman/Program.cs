using System;
using System.Data;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.IO;

namespace Pacman
{
    class Program
    {
        static void Main(string[] args)
        {
            
            using (var window = new RenderWindow(new VideoMode(828, 900), "Pacman")) 
            {
                window.Closed += (o, e) => window.Close();
                FileStream file = File.Open("highscore.txt", FileMode.OpenOrCreate);
                
                file.Close();
                Scene scene = new Scene();
                scene.Loader.Load("maze");
                Clock clock = new Clock();
                window.SetView(new View(new FloatRect(18,0,414,450)));
                while (window.IsOpen)
                {
                    
                    window.DispatchEvents();

                    float deltaTime = clock.Restart().AsSeconds();
                    if (deltaTime > 0.01f) deltaTime = 0.01f;
                    
                    scene.UpdateAll(deltaTime);
                    
                    window.Clear(new Color(223, 246, 245));
                    // TODO:  Drawing
                    scene.RenderAll(window);
                    window.Display();
                }
            }

            
        }
    }
}
