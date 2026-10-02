using System;
using System.Collections.Generic;
using UnityEngine;

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
public class LevelData : ScriptableObject
{
    [SerializeField] private BoardConfig boardConfig;
    [SerializeField, Min(0.1f)] private float spawnInterval = 2f;

    [SerializeField] private List<DefenceItemEntry> defenceItems = new List<DefenceItemEntry>();
    [SerializeField] private List<EnemyEntry> enemies = new List<EnemyEntry>();

    public BoardConfig BoardConfig => boardConfig;
    public float SpawnInterval => spawnInterval;
    public IReadOnlyList<DefenceItemEntry> DefenceItems => defenceItems;
    public IReadOnlyList<EnemyEntry> Enemies => enemies;
}
