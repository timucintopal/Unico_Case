using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        Camera MainCamera => Camera.main;
        
        [SerializeField] private LayerMask cellLayer;
        
        Cell lastCell;

        private void OnEnable()
        {
            EventBus<GameStartRequestedEvent>.Subscribe(Reset);
        }
        
        
        private void OnDisable()
        {
            EventBus<GameStartRequestedEvent>.Unsubscribe(Reset);
        }

        private void Reset(GameStartRequestedEvent obj)
        {
            lastCell = null;
        }


        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
 
            Debug.Log("Mouse Click");
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
 
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cellLayer))
            {
                //Debug.Log(hit.collider.gameObject.name);
                EventBus<CellClickedEvent>.Publish(new CellClickedEvent(hit.collider.gameObject));
            }
        }
    }
 
    public readonly struct CellClickedEvent : IEvent
    {
        public readonly GameObject Cell;
 
        public CellClickedEvent(GameObject cell)
        {
            Cell = cell;
        }
    }


}
