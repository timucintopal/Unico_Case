using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.ScriptableObject
{
    [CreateAssetMenu(menuName = "Board Defence/Level Catalog")]
    public class LevelCatalog : UnityEngine.ScriptableObject
    {
        [SerializeField] private List<LevelData> levels;

        public int Count => levels.Count;
        public LevelData GetLevel(int levelIndex) => levels[levelIndex % levels.Count];
    }
}
