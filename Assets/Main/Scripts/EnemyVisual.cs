using DG.Tweening;
using UnityEngine;

namespace Main.Scripts
{
    namespace Main.Scripts
    {
        public class EnemyVisual : MonoBehaviour
        {
            private const float SpawnDuration = 0.2f;
            private const float FlashDuration = 0.08f;

            [SerializeField] private Transform model;
            [SerializeField] private Renderer modelRenderer;

            private Material material;
            private Vector3 scale;

            private void Awake()
            {
                scale = model.localScale;
                material = modelRenderer.material;
            }

            private void OnDestroy()
            {
                model.DOKill();
                material.DOKill();
                Destroy(material);
            }

            public void PlaySpawn()
            {
                model.localScale = Vector3.zero;
                model.DOScale(scale, SpawnDuration).SetEase(Ease.OutBounce);
            }

            public void PlayHit()
            {
                material.DOKill(true);
                material.DOColor(Color.white, FlashDuration).SetLoops(2, LoopType.Yoyo);
            }
        }
    }
}