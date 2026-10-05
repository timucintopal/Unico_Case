using Main.Scripts.Board;
using Main.Scripts.Core;
using Main.Scripts.Data;
using Main.Scripts.Defence;
using Main.Scripts.Enemy;
using UnityEngine;

namespace Main.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private DefenceItemSpawner defenceItemSpawner;

        [SerializeField] private BoardGenerator board;

        [SerializeField] private LevelCatalog levelCatalog;
        [SerializeField] private LevelData currentLevelData;

        private readonly EnemyRegistry enemyRegistry = new();

        private void OnEnable()
        {
            EventBus.OnGameStateChanged += HandleGameStateChanged;
            EventBus.OnEnemyKilled += EnemyKilled;
            EventBus.OnEnemyReachedBase += EnemyReachedBase;
        }

        private void OnDisable()
        {
            EventBus.OnGameStateChanged -= HandleGameStateChanged;
            EventBus.OnEnemyKilled -= EnemyKilled;
            EventBus.OnEnemyReachedBase -= EnemyReachedBase;
        }

        private void EnemyKilled(Enemy.Enemy enemy)
        {
            enemyRegistry.Remove(enemy);

            if (enemySpawner.IsFinished && enemyRegistry.Count == 0)
                EndLevel(true);
        }

        private void EnemyReachedBase(Enemy.Enemy enemy)
        {
            EndLevel(false);
        }

        private void EndLevel(bool won)
        {
            if (won) SaveSystem.LevelIndex++;

            enemySpawner.Clear();
            defenceItemSpawner.Clear();
            EventBus.RaiseLevelEnded(won);
        }

        public void Init(BoardGenerator boardGenerator)
        {
            board = boardGenerator;
            enemySpawner.Init(board, enemyRegistry);
            defenceItemSpawner.Init(enemyRegistry);
        }

        private void HandleGameStateChanged(GameState state)
        {
            if (state == GameState.Game)
            {
                currentLevelData = levelCatalog.GetLevel(SaveSystem.LevelIndex);
                enemySpawner.SpawnEnemies(currentLevelData.Enemies, currentLevelData.EnemySpawnInterval);
                EventBus.RaiseLevelLoaded(currentLevelData);
            }
        }
    }
}