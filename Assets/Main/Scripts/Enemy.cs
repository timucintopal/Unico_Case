using DG.Tweening;
using UnityEngine;

namespace Main.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private const float BaseRow = -0.5f;
        [SerializeField] private Transform model;
        private BoardGenerator board;

        private EnemyData data;
        private int health;

        private Vector3 scale;

        public int Column { get; private set; }
        public float Row { get; private set; }
        public bool IsAlive => health > 0;

        private void Awake()
        {
            scale = model.localScale;
        }

        private void Update()
        {
            Move();

            if (Row > BaseRow) return;

            enabled = false;
            EventBus.RaiseEnemyReachedBase(this);
        }

        public void Init(EnemyData enemyData, BoardGenerator boardGenerator, int column, float startRow)
        {
            data = enemyData;
            board = boardGenerator;
            health = data.Health;
            Column = column;
            Row = startRow;
            transform.position = board.GridToWorld(Column, Row);

            SpawnEffect();
        }

        private void SpawnEffect()
        {
            model.localScale = Vector3.zero;
            model.DOScale(scale, .2f).SetEase(Ease.OutBounce);
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
            if (IsAlive) return;

            enabled = false;
            EventBus.RaiseEnemyKilled(this);
        }

    }
}