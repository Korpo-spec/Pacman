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
            
            using (var window = new RenderWindow(new VideoMode(828, 900), "Platformer")) 
            {
                window.Closed += (o, e) => window.Close();

                Clock clock = new Clock();
                while (window.IsOpen)
                {
                    window.DispatchEvents();

                    float deltaTime = clock.Restart().AsSeconds();
                    
                    window.Clear();
                    // TODO:  Drawing
                    
                    window.Display();
                }
            }
        }
    }
}
