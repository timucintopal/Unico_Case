using Main.Scripts;
using UnityEngine;

public enum AttackDirection
{
    Forward,
    All
}

[CreateAssetMenu(fileName = "DefenceItem", menuName = "Board Defence/Defence Item")]
public class DefenceItemData : ScriptableObject
{
    [SerializeField] [Min(1)] private int damage = 3;
    [SerializeField] [Min(1)] private int range = 4;
    [SerializeField] [Min(0.1f)] private float interval = 3f;
    [SerializeField] private AttackDirection direction = AttackDirection.Forward;

    [SerializeField] private DefenceItem prefab;
    [SerializeField] private Sprite icon;
    [SerializeField] private string name;

    public int Damage => damage;
    public int Range => range;
    public float Interval => interval;
    public AttackDirection Direction => direction;
    public DefenceItem Prefab => prefab;
    public Sprite Icon => icon;
    public string Name => name;
}