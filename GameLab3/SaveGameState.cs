
namespace GameLab3.Utils
{
    public class GameController
    {
        private GameState _gameState;

        public void SaveGameState()
        {
            string filePath = "gameState.dat";
            _gameState.Save(filePath);
        }

        public void LoadGameState()
        {
            string filePath = "gameState.dat";
            _gameState = GameState.Load(filePath);
        }
    }
}
