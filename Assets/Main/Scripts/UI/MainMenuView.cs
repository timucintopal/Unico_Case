using System;
using DG.Tweening;
using Main.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] Button playButton;
        [SerializeField] CanvasGroup canvasGroup;
        
        [SerializeField] TextMeshProUGUI levelLabelText;
        
        private float fadeDuration = 0.4f;
        private Ease fadeEase = Ease.OutQuad;
        private Tween fadeTween;

        private void OnEnable()
        {
            playButton.onClick.AddListener(TryStartGame);
            EventBus.OnGameStateChanged += SwitchView;
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(TryStartGame);
            EventBus.OnGameStateChanged -= SwitchView;
        }

        private void Awake()
        {
            RefreshLevelLabel();
        }

        void SwitchView(GameState state)
        {
            if(state == GameState.Game)
                Hide();
            else if(state == GameState.MainMenu)
                Show();
        }
        
        private void Hide()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
 
            fadeTween?.Kill();
            fadeTween = canvasGroup.DOFade(0f, fadeDuration).SetEase(fadeEase);
        }
 
        private void Show()
        {
            fadeTween?.Kill();
            fadeTween = canvasGroup.DOFade(1f, fadeDuration)
                .SetEase(fadeEase)
                .OnComplete(() =>
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                });

            RefreshLevelLabel();
        }

        void RefreshLevelLabel()
        {
            levelLabelText.text = Constants.LevelLabel + (SaveSystem.LevelIndex + 1);
        }

        void TryStartGame()
        {
            Debug.Log("PLAY PRESSED");
            EventBus.OnGameStartRequested?.Invoke();
        }
    }
}
