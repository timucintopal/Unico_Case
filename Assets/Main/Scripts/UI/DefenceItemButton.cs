using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class DefenceItemButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;

        private int count;

        public DefenceItemData Data { get; private set; }

        public void Initialize(DefenceItemData defenceItemData, int count)
        {
            Data = defenceItemData;
            this.count = count;
            button.onClick.AddListener(Select);
            RefreshStatus();
        }
        
        public void Consume()
        {
            count--;
            RefreshStatus();
        }

        private void Select()
        {
            EventBus.RaiseItemSelected(this);
        }

        private void RefreshStatus()
        {
            text.text = $"{Data.Name} x{count}";
            button.interactable = count > 0;
        }
    }
}