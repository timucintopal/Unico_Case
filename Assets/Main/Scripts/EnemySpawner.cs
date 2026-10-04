using System.Collections.Generic;
using Main.Scripts.ScriptableObject;
using NUnit.Framework;
using UnityEngine;

namespace Main.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyRegistry registry;
        [SerializeField] private BoardGenerator board;

        [SerializeField] private float spawnInterval;
        private float timer = 0;
        
        private readonly Queue<EnemyData> spawnQueue = new Queue<EnemyData>();
        
        private bool IsFinished => spawnQueue.Count == 0;
        public void Init(BoardGenerator boardGenerator, EnemyRegistry enemyRegistry)
        {
            board = boardGenerator;
            registry = enemyRegistry;
        }

        public void SpawnEnemies(IReadOnlyList<EnemyEntry> enemyEntry, float spawnInterval)
        {
            timer = 0;
            registry.Clear();
            spawnQueue.Clear();
            this.spawnInterval = spawnInterval;

            foreach (var enemyData in SetList(enemyEntry))
            {
                spawnQueue.Enqueue(enemyData);
            }
        }

        private void Update()
        {
            if(IsFinished) return;
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                var enemy = spawnQueue.Dequeue();
                Debug.Log("Spawning enemy " + enemy.name);
                timer = 0;
                Spawn(enemy);
            }
        }

        private void Spawn(EnemyData data)
        {
            Debug.Log("ENEMY NAME " + data.Prefab.name);
            Enemy enemy = Instantiate(data.Prefab, transform);
            enemy.Init(data, board.GetRandomTopCellPosition());
            registry.Add(enemy);

        }

        private List<EnemyData> SetList(IReadOnlyList<EnemyEntry> entries)
        {
            var list = new List<EnemyData>();
            foreach (EnemyEntry entry in entries)
                for (int i = 0; i < entry.Count; i++)
                {
                    Debug.Log("ADDED ENEMY " + entry.Enemy.name);
                    list.Add(entry.Enemy);
                }
 
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
 
            return list;

        }
    }
}