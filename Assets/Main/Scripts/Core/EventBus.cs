using System;
using Main.Scripts.Board;
using Main.Scripts.Data;
using Main.Scripts.Managers;
using Main.Scripts.UI;

namespace Main.Scripts.Core
{
    public static class EventBus
    {
        public static event Action OnGameStartRequested;
        public static event Action<GameState> OnGameStateChanged;
        public static event Action<LevelData> OnLevelLoaded;
        public static event Action<DefenceItemButton> OnItemDragStarted;
        public static event Action<DefenceItemButton, Cell> OnItemDropped;
        public static event Action<Enemy.Enemy> OnEnemyKilled;
        public static event Action<Enemy.Enemy> OnEnemyReachedBase;
        public static event Action<bool> OnLevelEnded;
        public static event Action OnMenuRequested;

        public static void RaiseGameStartRequested()
        {
            OnGameStartRequested?.Invoke();
        }

        public static void RaiseGameStateChanged(GameState state)
        {
            OnGameStateChanged?.Invoke(state);
        }

        public static void RaiseLevelLoaded(LevelData level)
        {
            OnLevelLoaded?.Invoke(level);
        }
        
        public static void RaiseItemDragStarted(DefenceItemButton button)
        {
            OnItemDragStarted?.Invoke(button);
        }

        public static void RaiseItemDropped(DefenceItemButton button, Cell cell)
        {
            OnItemDropped?.Invoke(button, cell);
        }

        public static void RaiseEnemyKilled(Enemy.Enemy enemy)
        {
            OnEnemyKilled?.Invoke(enemy);
        }

        public static void RaiseEnemyReachedBase(Enemy.Enemy enemy)
        {
            OnEnemyReachedBase?.Invoke(enemy);
        }

        public static void RaiseLevelEnded(bool won)
        {
            OnLevelEnded?.Invoke(won);
        }
        
        public static void RaiseMenuRequested()
        {
            OnMenuRequested?.Invoke();
        }
    }
}