using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        Camera MainCamera => Camera.main;
        
        [SerializeField] private LayerMask cellLayer;
        
        [SerializeField] private Cell lastCell;

        private void OnEnable()
        {
            EventBus.OnGameStartRequested += Reset;
        }

        private void OnDisable()
        {
            EventBus.OnGameStartRequested -= Reset;
        }

        private void Reset()
        {
            lastCell = null;
        }


        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
 
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cellLayer))
            {
                var newCell = hit.collider.GetComponent<Cell>();

                if (newCell == lastCell) return;
                Debug.Log("Mouse Click " + hit.collider.name);
                lastCell = newCell;
                EventBus.OnCellHoverChanged(lastCell);
            }
        }
    }
}
