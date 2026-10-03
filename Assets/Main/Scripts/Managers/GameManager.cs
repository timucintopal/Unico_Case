using Main.Scripts.Managers;
using Main.Scripts.ScriptableObject;
using UnityEngine;
using UnityEngine.Events;

namespace Main.Scripts
{

    public enum GameState
    {
        MainMenu,
        Game,
        GameOver,
    }
    public class GameManager : MonoBehaviour
    {
        private InputManager InputPrefab => Resources.Load<InputManager>(Constants.InputPrefabPath);
        private BoardGenerator BoardPrefab => Resources.Load<BoardGenerator>(Constants.BoardPrefabPath);
        private LevelManager LevelPrefab => Resources.Load<LevelManager>(Constants.LevelManagerPrefabPath);
        
        
        
        private GameState GameState { get; set; } = GameState.MainMenu;
        
        InputManager InputManager { get; set; }
        BoardGenerator Board { get; set; }
        LevelManager LevelManager { get; set; }
        
        GameState _currentGameState = GameState.MainMenu;

        #region Events

        public static UnityAction OnMenu;
        public static UnityAction OnGame;
        public static UnityAction OnGameOver;

        #endregion

        private void OnEnable()
        {
            EventBus.OnGameStartRequested += StartGame;
        }
        
        private void OnDisable()
        {
            EventBus.OnGameStartRequested -= StartGame;
        }

        private void StartGame()
        {
            if(_currentGameState == GameState.Game) return;
            _currentGameState = GameState.Game;
            EventBus.OnGameStateChanged?.Invoke(_currentGameState);
            
            if(Board == null)
                InitBoard();
            
            if(InputManager == null)
                InputManager = Instantiate(InputPrefab);
            
            if(LevelManager == null)
            {
                LevelManager = Instantiate(LevelPrefab);
            }
        }
        
        void InitBoard()
        {
            Board = Instantiate(BoardPrefab);
            Board.transform.localPosition = Vector3.zero;
        }
        
    }
}
