namespace ClickerGame.Game.Signals
{
    public class StartGameSignal
    {
        public string DifficultyName { get; }

        public StartGameSignal(string difficultyName)
        {
            DifficultyName = difficultyName;
        }
    }

    public class GameOverSignal
    {
        public string GameoverText { get; }

        public GameOverSignal(string gameoverText)
        {
            GameoverText = gameoverText;
        }
    }
}