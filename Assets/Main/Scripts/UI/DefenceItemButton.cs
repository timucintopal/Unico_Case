using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class DefenceItemButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Button button;
        [SerializeField] private Image background;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI countText;
        private int count;

        public DefenceItemData Data { get; private set; }

        public void Initialize(DefenceItemData defenceItemData, int count)
        {
            Data = defenceItemData;
            this.count = count;
            background.color = Data.Color;
            nameText.text = Data.Name;
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
            countText.text = count.ToString();
            button.interactable = count > 0;
        }
    }
}