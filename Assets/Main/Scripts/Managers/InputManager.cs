using Main.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private LayerMask cellLayer;

        private Camera mainCamera;
        private Plane boardPlane = new(Vector3.up, Vector3.zero);

        [SerializeField] private DefenceItemButton draggedButton;
        [SerializeField] private DefenceItem preview;
        [SerializeField] private Cell hoveredCell;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            EventBus.OnItemDragStarted += StartDrag;
        }

        private void OnDisable()
        {
            EventBus.OnItemDragStarted -= StartDrag;
        }

        private void Update()
        {
            if (draggedButton != null)
                UpdateDrag();
        }

        private void StartDrag(DefenceItemButton button)
        {
            draggedButton = button;
            preview = Instantiate(draggedButton.Data.Prefab);
            preview.enabled = false;
            UpdateDrag();
        }

        private void UpdateDrag()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var cell = GetCell(ray);

            SetHoveredCell(cell);
            MovePreview(ray, cell);

            if (Input.GetMouseButtonUp(0))
                EndDrag(cell);
        }

        private void EndDrag(Cell cell)
        {
            Destroy(preview.gameObject);
            SetHoveredCell(null);

            if (cell != null)
                EventBus.RaiseItemDropped(draggedButton, cell);

            draggedButton = null;
        }

        private Cell GetCell(Ray ray)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return null;

            return Physics.Raycast(ray, out var hit, Mathf.Infinity, cellLayer)
                ? hit.collider.GetComponent<Cell>()
                : null;
        }

        private void MovePreview(Ray ray, Cell cell)
        {
            if (cell != null && cell.CanPlace)
                preview.transform.position = cell.transform.position;
            else if (boardPlane.Raycast(ray, out var distance))
                preview.transform.position = ray.GetPoint(distance);
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