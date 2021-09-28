using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Pacman
{
    class Program
    {
        static void Main(string[] args)
        {
            
            using (var window = new RenderWindow(new VideoMode(828, 900), "Pacman")) 
            {
                window.Closed += (o, e) => window.Close();
                
                Scene scene = new Scene();
                scene.Loader.Load("maze");
                Clock clock = new Clock();
                while (window.IsOpen)
                {
                    
                    window.DispatchEvents();

                    float deltaTime = clock.Restart().AsSeconds();
                    
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
