using System;
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
        public Vector3 Position {
            get
            {
                Debug.Log("GET POSITION OF " + gameObject.name); 
                return transform.position;    
            }
            
        }

        private bool _isHovered = false;

        private void OnEnable()
        {
            EventBus.OnCellHoverChanged += Hover;
        }

        private void OnDisable()
        {
            EventBus.OnCellHoverChanged -= Hover;
        }

        void Hover(Cell cell)
        {
            if(cell != this)
            {
                if (!_isHovered) return;
                _isHovered = false;
                RefreshMainColor();
                return;
            }

            if (!CanPlace) return;
            _isHovered = true;
            _spriteRenderer.color = _cellColorConfig.FillableColor;
        }

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
