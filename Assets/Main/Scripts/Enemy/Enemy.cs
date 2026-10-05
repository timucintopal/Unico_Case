using UnityEngine;

namespace Main.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private const float BaseRow = -0.5f;

        [SerializeField] private EnemyVisual visual;

        private BoardGenerator board;
        private EnemyData data;
        private int health;

        public int Column { get; private set; }
        public float Row { get; private set; }
        public bool IsAlive => health > 0;

        private void Update()
        {
            Move();

            if (Row > BaseRow) return;

            enabled = false;
            EventBus.RaiseEnemyReachedBase(this);
            Destroy(gameObject);
        }

        public void Init(EnemyData enemyData, BoardGenerator boardGenerator, int column, float startRow)
        {
            data = enemyData;
            board = boardGenerator;
            health = data.Health;
            Column = column;
            Row = startRow;
            transform.position = board.GridToWorld(Column, Row);

            visual.PlaySpawn();
        }

        private void Move()
        {
            Row -= data.Speed * Time.deltaTime;
            transform.position = board.GridToWorld(Column, Row);
        }

        public void TakeDamage(int amount)
        {
            if (!IsAlive) return;

            health -= amount;
            visual.PlayHit();
            if (IsAlive)
                return;

            enabled = false;
            EventBus.RaiseEnemyKilled(this);
            visual.PlayDeath(() => Destroy(gameObject));
        }
    }
}