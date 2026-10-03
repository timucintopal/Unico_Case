using System.Collections.Generic;
using Main.Scripts.ScriptableObject;
using UnityEngine;

namespace Main.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyRegistry registry;
        [SerializeField] private BoardGenerator board;

        [SerializeField] private IReadOnlyList<EnemyEntry> enemyEntries;
        [SerializeField] private float spawnInterval;
        public void Init(BoardGenerator boardGenerator, EnemyRegistry enemyRegistry)
        {
            board = boardGenerator;
            registry = enemyRegistry;
        }

        public void SpawnEnemies(IReadOnlyList<EnemyEntry> enemyEntry, float spawnInterval)
        {
            registry.Clear();
            enemyEntries = enemyEntry;
            this.spawnInterval = spawnInterval;
        }

    }
}