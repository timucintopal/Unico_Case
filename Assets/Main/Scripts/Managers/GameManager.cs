using UnityEngine;

namespace Main.Scripts.Managers
{
    public enum GameState
    {
        MainMenu,
        Game,
        GameOver
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputManager inputPrefab;
        [SerializeField] private BoardGenerator boardPrefab;
        [SerializeField] private LevelManager levelPrefab;
        private BoardGenerator board;

        private InputManager inputManager;
        private LevelManager levelManager;

        private GameState CurrentState { get; set; } = GameState.MainMenu;

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
            if (CurrentState == GameState.Game) return;

            InitSystem();
            ChangeState(GameState.Game);
        }

        private void InitSystem()
        {
            if (board == null)
                board = Instantiate(boardPrefab, Vector3.zero, Quaternion.identity);

            if (inputManager == null)
                inputManager = Instantiate(inputPrefab);

            if (levelManager == null)
            {
                levelManager = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
                levelManager.Init(board);
            }
        }

        private void ChangeState(GameState newState)
        {
            CurrentState = newState;
            EventBus.OnGameStateChanged?.Invoke(CurrentState);
        }
    }
}