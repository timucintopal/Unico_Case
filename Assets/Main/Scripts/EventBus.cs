using System;
using Main.Scripts.Managers;
using Main.Scripts.ScriptableObject;
using Main.Scripts.UI;
using UnityEngine.Events;

namespace Main.Scripts
{
    public static class EventBus
    {
        public static event Action OnGameStartRequested;
        public static event Action<GameState> OnGameStateChanged;
        public static event Action<LevelData> OnLevelLoaded;

        public static event Action<Cell> OnCellClicked;
        public static event Action<DefenceItemButton> OnItemSelected;
        public static event Action<DefenceItemButton> OnItemDragStarted;
        public static event Action<DefenceItemButton, Cell> OnItemDropped;

        public static event Action<Enemy> OnEnemyKilled;
        public static event Action<Enemy> OnEnemyReachedBase;

        public static event Action<bool> OnLevelEnded;

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

        public static void RaiseCellClicked(Cell cell)
        {
            OnCellClicked?.Invoke(cell);
        }

        public static void RaiseItemDragStarted(DefenceItemButton button)
        {
            OnItemDragStarted?.Invoke(button);
        }
        
        public static void RaiseItemDropped(DefenceItemButton button, Cell cell)
        {
            OnItemDropped?.Invoke(button,cell);
        }

        public static void RaiseEnemyKilled(Enemy enemy)
        {
            OnEnemyKilled?.Invoke(enemy);
        }

        public static void RaiseEnemyReachedBase(Enemy enemy)
        {
            OnEnemyReachedBase?.Invoke(enemy);
        }

        public static void RaiseItemSelected(DefenceItemButton defenceItemButton)
        {
            OnItemSelected?.Invoke(defenceItemButton);
        }

        public static void RaiseOnLevelEnded(bool won)
        {
            OnLevelEnded?.Invoke(won);
        }
    }
}