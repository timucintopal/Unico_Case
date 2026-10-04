using Main.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private Button menuButton;

        private void Awake()
        {
            panel.SetActive(false);
        }

        private void OnEnable()
        {
            menuButton.onClick.AddListener(EventBus.RaiseMenuRequested);
            EventBus.OnLevelEnded += Show;
            EventBus.OnGameStateChanged += Hide;
        }

        private void OnDisable()
        {
            menuButton.onClick.RemoveListener(EventBus.RaiseMenuRequested);
            EventBus.OnLevelEnded -= Show;
            EventBus.OnGameStateChanged -= Hide;
        }

        private void Show(bool won)
        {
            resultText.text = won ? Constants.SuccessLabel : Constants.FailLabel;
            panel.SetActive(true);
        }

        private void Hide(GameState state)
        {
            if (state != GameState.GameOver)
                panel.SetActive(false);
        }
    }
}