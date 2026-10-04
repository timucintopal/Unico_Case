using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class DefenceItemButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;

        private int count;

        public DefenceItemData Data { get; private set; }

        public void Initialize(DefenceItemData defenceItemData, int count)
        {
            Data = defenceItemData;
            this.count = count;
            RefreshStatus();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (count > 0)
                EventBus.RaiseItemDragStarted(this);
        }

        public void Consume()
        {
            count--;
            RefreshStatus();
        }

        private void RefreshStatus()
        {
            text.text = $"{Data.Name} x{count}";
            button.interactable = count > 0;
        }
    }
}