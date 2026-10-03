using System.Collections.Generic;

namespace Main.Scripts
{
    public class EnemyRegistry
    {
        private readonly List<Enemy> enemies = new List<Enemy>();

        public IReadOnlyList<Enemy> Enemies => enemies;
        public int Count => enemies.Count;

        public void Add(Enemy enemy) => enemies.Add(enemy);
        public void Remove(Enemy enemy) => enemies.Remove(enemy);
        public void Clear() => enemies.Clear();
    }
}