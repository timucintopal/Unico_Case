using Main.Scripts.ScriptableObject;
using UnityEngine;

namespace Main.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
        
        [SerializeField] private BoardGenerator board;
        
        [SerializeField] private LevelCatalog levelCatalog;
        [SerializeField] private LevelData currentLevelData;
        
        private readonly EnemyRegistry enemyRegistry = new EnemyRegistry();

        private void OnEnable()
        {
            EventBus.OnGameStateChanged += HandleGameStateChanged;
        }

        private void OnDisable()
        {
            EventBus.OnGameStateChanged -= HandleGameStateChanged;
        }
        
        public void Init(BoardGenerator boardGenerator)
        {
            board = boardGenerator;
            enemySpawner.Init(boardGenerator, enemyRegistry);
        }
        
        private void HandleGameStateChanged(GameState state)
        {
            if (state == GameState.Game)
            {
                currentLevelData = levelCatalog.GetLevel(SaveSystem.LevelIndex);
                enemySpawner.SpawnEnemies(currentLevelData.Enemies, currentLevelData.EnemySpawnInterval);
            }
        }
    }
}
