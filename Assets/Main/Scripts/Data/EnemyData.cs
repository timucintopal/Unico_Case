using Main.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Board Defence/Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] [Min(1)] private int health = 3;
    [SerializeField] [Min(0.01f)] private float speed = 1f;

    [SerializeField] private Enemy prefab;

    public int Health => health;
    public float Speed => speed;
    public Enemy Prefab => prefab;
}