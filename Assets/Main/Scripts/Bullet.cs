using UnityEngine;

namespace Main.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private TrailRenderer trail;
        [SerializeField] private float speed = 15f;

        private Enemy target;
        private int damage;

        private void OnEnable()
        {
            trail.Clear();
        }

        private void Update()
        {
            if (target == null || !target.IsAlive)
            {
                Pooler.Instance.Release(this);
                return;
            }

            var destination = target.transform.position;
            destination.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (transform.position != destination) return;

            target.TakeDamage(damage);
            Pooler.Instance.Release(this);
        }

        public void Init(Enemy enemy, int damageAmount)
        {
            target = enemy;
            damage = damageAmount;
        }
    }
}