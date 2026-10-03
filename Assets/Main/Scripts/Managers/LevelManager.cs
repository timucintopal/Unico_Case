using Main.Scripts.ScriptableObject;
using UnityEngine;

namespace Main.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelCatalog levelCatalog;
        [SerializeField] private LevelData currentLevelData;

        private void OnEnable()
        {
            EventBus.OnGameStateChanged += HandleGameStateChanged;
        }

        private void OnDisable()
        {
            EventBus.OnGameStateChanged -= HandleGameStateChanged;
        }
        
        private void HandleGameStateChanged(GameState state)
        {
            if (state == GameState.Game)
            {
                currentLevelData = levelCatalog.GetLevel(SaveSystem.LevelIndex);
            }
                
        }
    }
}
