using Main.Scripts.Managers;
using UnityEngine.Events;

namespace Main.Scripts
{
    public static class EventBus
    {
        public static UnityAction OnGameStartRequested;
        public static UnityAction<GameState> OnGameStateChanged;

        public static UnityAction<Cell> OnCellHoverChanged;
        public static UnityAction<Cell> OnCellReleased;

        public static UnityAction<DefenceItemData> OnItemDragStarted;
    }
}