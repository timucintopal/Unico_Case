using System;
using Main.Scripts.ScriptableObject;
using UnityEngine;

namespace Main.Scripts.UI
{
    public class FooterView : MonoBehaviour
    {
        [SerializeField] private Transform buttonParent;
        [SerializeField] private DefenceItemButton buttonPrefab;

        private void OnEnable()
        {
            EventBus.OnLevelLoaded += Build;
        }
        private void OnDisable()
        {
            EventBus.OnLevelLoaded -= Build;
        }

        private void Build(LevelData level)
        {
            foreach (Transform child in buttonParent)
                Destroy(child.gameObject);

            foreach (var entry in level.DefenceItems)
                Instantiate(buttonPrefab, buttonParent).Initialize(entry.Item, entry.Count);
        }
    }
}