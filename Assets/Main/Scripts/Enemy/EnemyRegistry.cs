using System.Collections.Generic;

namespace Main.Scripts.Enemy
{
    public class EnemyRegistry
    {
        private readonly List<Scripts.Enemy.Enemy> enemies = new();

        public IReadOnlyList<Scripts.Enemy.Enemy> Enemies => enemies;
        public int Count => enemies.Count;

        public void Add(Scripts.Enemy.Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void Remove(Scripts.Enemy.Enemy enemy)
        {
            enemies.Remove(enemy);
        }

        public void Clear()
        {
            enemies.Clear();
        }
    }
}