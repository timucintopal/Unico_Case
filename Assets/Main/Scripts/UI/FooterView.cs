using System;
using System.Collections.Generic;
using Main.Scripts.ScriptableObject;
using UnityEngine;

namespace Main.Scripts.UI
{
    public class FooterView : MonoBehaviour
    {
        [SerializeField] private Transform buttonParent;
        [SerializeField] private DefenceItemButton buttonPrefab;
        
        private readonly List<DefenceItemButton> buttons = new();

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
            var entries = level.DefenceItems;

            while (buttons.Count < entries.Count)
                buttons.Add(Instantiate(buttonPrefab, buttonParent));

            while (buttons.Count > entries.Count)
            {
                var last = buttons[^1];
                buttons.RemoveAt(buttons.Count - 1);
                Destroy(last.gameObject);
            }

            for (var i = 0; i < entries.Count; i++)
                buttons[i].Initialize(entries[i].Item, entries[i].Count);
        }
    }
}