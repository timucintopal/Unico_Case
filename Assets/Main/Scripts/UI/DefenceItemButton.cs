using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class DefenceItemButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;
        
        DefenceItemData _defenceItemData;

        void Initialize(DefenceItemData defenceItemData)
        {
            _defenceItemData = defenceItemData;
        }
        
    }
}
