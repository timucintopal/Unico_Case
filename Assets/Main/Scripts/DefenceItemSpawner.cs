using System.Collections.Generic;
using Main.Scripts.UI;
using UnityEngine;

namespace Main.Scripts
{
    public class DefenceItemSpawner : MonoBehaviour
    {
        private EnemyRegistry registry;

        private readonly Dictionary<DefenceItem, Cell> placed = new();

        private void OnEnable()
        {
            EventBus.OnItemDropped += Place;
        }

        private void OnDisable()
        {
            EventBus.OnItemDropped -= Place;
        }

        public void Init(EnemyRegistry enemyRegistry)
        {
            registry = enemyRegistry;
        }

        private void Place(DefenceItemButton button, Cell cell)
        {
            if (!cell.CanPlace) return;

            var data = button.Data;
            var item = Pooler.Instance.Get(data.Prefab, cell.transform.position);
            item.Init(data, registry, cell);
            cell.Occupy();
            placed[item] = cell;
            button.Consume();
        }

        public void Clear()
        {
            foreach (var (item, cell) in placed)
            {
                Pooler.Instance.Release(item);
                cell.Clear();
            }

            placed.Clear();
        }
    }
}