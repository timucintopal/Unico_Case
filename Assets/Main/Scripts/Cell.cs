using UnityEngine;

namespace Main.Scripts
{
    public enum CellState
    {
        Empty,
        Blocked,
        Filled,
    }
    public class Cell : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] CellColorConfig _cellColorConfig;
        
        public CellState State { get; private set; }
        
        public bool IsPlaceable { get;  private set; }
        public bool IsOccupied { get; private set; }
        public bool CanPlace => IsPlaceable && !IsOccupied;

        public void Initialize(int column, int row, Vector3 scale, bool isPlaceable)
        {
            IsPlaceable = isPlaceable;

            transform.localScale = scale;
            State = IsPlaceable ? CellState.Empty : CellState.Blocked;
            name = $"Cell ({column}, {row})";
            
            RefreshMainColor();
        }

        void RefreshMainColor()
        {
            _spriteRenderer.color = IsPlaceable ?  _cellColorConfig.EmptyColor : _cellColorConfig.BlockedColor;
        }
    }
}
