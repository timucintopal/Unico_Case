using System;
using Main.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Main.Scripts
{

    public enum State
    {
        MainMenu,
        Game,
        GameOver,
    }
    public class GameManager : MonoBehaviour
    {
        InputManager InputPrefab => Resources.Load<InputManager>("System/InputManager");
        BoardGenerator BoardPrefab => Resources.Load<BoardGenerator>("Props/Board");
        
        InputManager InputManager { get; set; }

        BoardGenerator Board { get; set; }
        
        State currentState = State.MainMenu;


        #region Events

        public static UnityAction onMenu;
        public static UnityAction onGame;
        public static UnityAction onGameOver;

        #endregion

        private void OnEnable()
        {
            EventBus<GameStartRequestedEvent>.Subscribe(StartGame);
        }
        
        private void OnDisable()
        {
            EventBus<GameStartRequestedEvent>.Unsubscribe(StartGame);
        }

        private void StartGame(GameStartRequestedEvent obj)
        {
            if(Board == null)
                InitBoard();
            
            if(InputManager == null)
                InputManager = Instantiate(InputPrefab);
            
        }
        
        void InitBoard()
        {
            Board = Instantiate(BoardPrefab);
            Board.transform.localPosition = Vector3.zero;
        }
        
    }
}
