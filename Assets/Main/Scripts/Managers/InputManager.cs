using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private LayerMask cellLayer;

        [SerializeField] private Cell lastCell;
        private Camera MainCamera => Camera.main;

        private void Reset()
        {
            lastCell = null;
        }


        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            var ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, cellLayer))
            {
                var newCell = hit.collider.GetComponent<Cell>();

                if (newCell == lastCell) return;
                lastCell = newCell;
                EventBus.OnCellHoverChanged(lastCell);
            }
        }

        private void OnEnable()
        {
            EventBus.OnGameStartRequested += Reset;
        }

        private void OnDisable()
        {
            EventBus.OnGameStartRequested -= Reset;
        }
    }
}