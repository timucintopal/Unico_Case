using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.ScriptableObject
{
    [Serializable]
    public struct DefenceItemEntry
    {
        public DefenceItemData Item;
        [Min(1)] public int Count;
    }

    [Serializable]
    public struct EnemyEntry
    {
        public EnemyData Enemy;
        [Min(1)] public int Count;
    }

    [CreateAssetMenu(fileName = "Level", menuName = "Board Defence/Level")]
    public class LevelData : UnityEngine.ScriptableObject
    {
        [SerializeField] [Min(0.1f)] private float enemySpawnInterval = 2f;

        [SerializeField] private List<DefenceItemEntry> defenceItems = new();
        [SerializeField] private List<EnemyEntry> enemies = new();

        public IReadOnlyList<DefenceItemEntry> DefenceItems => defenceItems;
        public IReadOnlyList<EnemyEntry> Enemies => enemies;
        public float EnemySpawnInterval => enemySpawnInterval;
    }
}