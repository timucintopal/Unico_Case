using UnityEngine;

namespace Main.Scripts
{
    public enum CellState
    {
        Empty,
        Blocked,
        Filled
    }

    public class Cell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CellColorConfig cellColorConfig;

        private bool _isHovered;

        public CellState State { get; private set; }

        public int Row { get; private set; }
        public int Column { get; private set; }
        
        public bool CanPlace => State == CellState.Empty ;
        
        public void Initialize(int column, int row, Vector3 scale, bool isPlaceable)
        {
            Row = row;
            Column = column;

            transform.localScale = scale;
            State = isPlaceable ? CellState.Empty : CellState.Blocked;
            
            name = "Cell_" + column + "_" + row;

            RefreshColor();
        }
        
        public void SetHover(bool isHovered)
        {
            _isHovered = isHovered;
            RefreshColor();
        }

        public void Occupy()
        {
            State = CellState.Filled;
            RefreshColor();
        }

        private void RefreshColor()
        {
            spriteRenderer.color = GetColor();
        }

        private Color GetColor()
        {
            if (_isHovered && State == CellState.Empty)
                return cellColorConfig.FillableColor;

            return State switch
            {
                CellState.Empty => cellColorConfig.EmptyColor,
                CellState.Filled => cellColorConfig.FilledColor,
                _ => cellColorConfig.BlockedColor
            };
        }
    }
}