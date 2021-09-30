namespace Pacman
{
    public delegate void ValueChangedEvent(Scene scene, int value);

    public delegate void ValueIncremented();

    public class EventManager
    {
        public event ValueChangedEvent GainScore;
        public event ValueChangedEvent LoseHealth;
        public event ValueIncremented CandyEaten;

        private int scoreGained;
        private int healthLost;
        private int candiesEaten;
        public void PublishGainedScore(int amount) => scoreGained += amount;
        public void PublishLoseHealth(int amount) => healthLost += amount;
        public void PublishCandyEaten() => candiesEaten++;

        public void HandleEvents(Scene scene)
        {
            if (healthLost != 0)
            {
                LoseHealth?.Invoke(scene, healthLost);
                healthLost = 0;
            }

            if (candiesEaten != 0)
            {
                CandyEaten?.Invoke();
                candiesEaten = 0;
            }
            
        }

        public void HandleLateEvents(Scene scene)
        {
            if (scoreGained != 0)
            {
                GainScore?.Invoke(scene, scoreGained);
                scoreGained = 0;
            }
        }
    }
}