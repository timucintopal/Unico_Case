using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts
{
    public class Pooler : MonoBehaviour
    {
        private readonly Dictionary<GameObject, Stack<GameObject>> pools = new();
        private readonly Dictionary<GameObject, GameObject> prefabByItem = new();

        public static Pooler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public T Get<T>(T prefab, Vector3 position) where T : Component
        {
            var pool = GetPool(prefab.gameObject);
            var item = pool.Count > 0 ? pool.Pop() : Create(prefab.gameObject, position);

            item.transform.position = position;
            item.SetActive(true);
            return item.GetComponent<T>();
        }
        
        public void Release(Component item)
        {
            item.gameObject.SetActive(false);
            GetPool(prefabByItem[item.gameObject]).Push(item.gameObject);
        }

        private GameObject Create(GameObject prefab, Vector3 position)
        {
            var item = Instantiate(prefab, position, Quaternion.identity, transform);
            prefabByItem[item] = prefab;
            return item;
        }

        private Stack<GameObject> GetPool(GameObject prefab)
        {
            if (!pools.TryGetValue(prefab, out var pool))
                pools[prefab] = pool = new Stack<GameObject>();

            return pool;
        }
    }
}