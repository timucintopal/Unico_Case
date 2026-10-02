using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] Button playButton;
        [SerializeField] CanvasGroup canvasGroup;
        
        private float fadeDuration = 0.4f;
        private Ease fadeEase = Ease.OutQuad;

        Tween fadeTween;

        private void OnEnable()
        {
            playButton.onClick.AddListener(TryStartGame);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(TryStartGame);
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
        }


        void TryStartGame()
        {
            Debug.Log("PLAY PRESSED");
            Hide();
            EventBus.OnGameStartRequested?.Invoke();
        }
    }
}
