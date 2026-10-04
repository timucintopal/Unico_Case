using DG.Tweening;
using UnityEngine;

namespace Main.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform model;

        private Vector3 scale;
        
        private const float BaseRow = -0.5f;
 
        private EnemyData data;
        private BoardGenerator board;
        private int health;
 
        public int Column { get; private set; }
        public float Row { get; private set; }
        public bool IsAlive => health > 0;

        private void Awake()
        {
            scale = model.localScale;
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

        void SpawnEffect()
        {
            model.localScale = Vector3.zero;
            model.DOScale(scale, .2f ).SetEase(Ease.OutBounce);
        }
 
        private void Update()
        {
            Move();
 
            if (Row <= BaseRow)
            {
                enabled = false;
                // EventBus.OnEnemyReachedBase?.Invoke(this);
            }
        }
 
        private void Move()
        {
            Row -= data.Speed * Time.deltaTime;
            transform.position = board.GridToWorld(Column, Row);
        }
 
        public void TakeDamage(int amount)
        {
            if (!IsAlive)
                return;
 
            health -= amount;
            if (!IsAlive)
            {
                enabled = false;
                // EventBus.OnEnemyKilled?.Invoke(this);
            }
        }
    }

}
