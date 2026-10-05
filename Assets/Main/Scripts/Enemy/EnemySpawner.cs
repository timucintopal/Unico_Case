using System.Collections.Generic;
using Main.Scripts.ScriptableObject;
using UnityEngine;

namespace Main.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private BoardGenerator board;

        [SerializeField] private float spawnInterval;

        private readonly Queue<EnemyData> spawnQueue = new();
        [SerializeField] private EnemyRegistry registry;
        private float timer;

        public bool IsFinished => spawnQueue.Count == 0;

        private void Update()
        {
            if (IsFinished) return;
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                var enemy = spawnQueue.Dequeue();
                timer = 0;
                Spawn(enemy);
            }
        }

        public void Init(BoardGenerator boardGenerator, EnemyRegistry enemyRegistry)
        {
            board = boardGenerator;
            registry = enemyRegistry;
        }

        public void SpawnEnemies(IReadOnlyList<EnemyEntry> enemyEntry, float spawnInterval)
        {
            timer = 0;
            spawnQueue.Clear();
            this.spawnInterval = spawnInterval;

            foreach (var enemyData in SetList(enemyEntry)) spawnQueue.Enqueue(enemyData);
        }

        private void Spawn(EnemyData data)
        {
            var enemy = Instantiate(data.Prefab, transform);
            enemy.Init(data, board, board.GetRandomColumn(), board.SpawnRow);
            registry.Add(enemy);
        }

        private List<EnemyData> SetList(IReadOnlyList<EnemyEntry> entries)
        {
            var list = new List<EnemyData>();
            foreach (var entry in entries)
                for (var i = 0; i < entry.Count; i++)
                    list.Add(entry.Enemy);

            for (var i = list.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }

            return list;
        }

        public void Clear()
        {
            spawnQueue.Clear();

            foreach (var enemy in registry.Enemies)
                Destroy(enemy.gameObject);

            registry.Clear();
        }
    }
}