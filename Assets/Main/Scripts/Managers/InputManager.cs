using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private LayerMask cellLayer;

        private Camera mainCamera;
        [SerializeField] private Cell hoveredCell;

        private void Awake()
        {
            mainCamera = Camera.main;
        }


        private void Reset()
        {
            SetHoveredCell(null);
        }
        
        private void Update()
        {
            var cell = GetCellUnderPointer();
            SetHoveredCell(cell);

            if (cell != null && Input.GetMouseButtonDown(0))
                EventBus.RaiseCellClicked(cell);
        }
        
        private Cell GetCellUnderPointer()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return null;

            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            return Physics.Raycast(ray, out var hit, Mathf.Infinity, cellLayer)
                ? hit.collider.GetComponent<Cell>()
                : null;
        }


        private void SetHoveredCell(Cell cell)
        {
            if (cell == hoveredCell) return;

            if (hoveredCell != null) hoveredCell.SetHover(false);
            hoveredCell = cell;
            if (hoveredCell != null) hoveredCell.SetHover(true);
        }
    }
}