using System;
using DG.Tweening;
using UnityEngine;

namespace Main.Scripts
{
    public class DefenceItem : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform muzzle;


        private DefenceItemData data;
        private EnemyRegistry registry;

        private int column;
        private int row;
        private float timer;

        private const float RecoilDistance = 0.15f;
        private const float RecoilDuration = 0.08f;

        private void OnDestroy()
        {
            muzzle.DOKill();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer < data.Interval) return;

            var target = FindTarget();
            if (target == null) return;

            timer = 0f;
            Pooler.Instance.Get(bulletPrefab, muzzle.position).Init(target, data.Damage);
            if (data.Direction == AttackDirection.All) LookAtTarget(target.transform);
            Recoil();
        }

        public void Init(DefenceItemData itemData, EnemyRegistry enemyRegistry, Cell cell)
        {
            data = itemData;
            registry = enemyRegistry;
            column = cell.Column;
            row = cell.Row;
            timer = data.Interval;

            if (itemData.Direction == AttackDirection.All)
                muzzle.rotation = Quaternion.identity;
        }

        private Enemy FindTarget()
        {
            Enemy closest = null;
            var closestDistance = float.MaxValue;

            foreach (var enemy in registry.Enemies)
            {
                if (!enemy.IsAlive || !IsInRange(enemy)) continue;

                var distance = Mathf.Abs(enemy.Row - row) + Mathf.Abs(enemy.Column - column);
                if (distance >= closestDistance) continue;

                closest = enemy;
                closestDistance = distance;
            }

            return closest;
        }

        private bool IsInRange(Enemy enemy)
        {
            var rowDelta = enemy.Row - row;
            var columnDelta = Mathf.Abs(enemy.Column - column);

            if (data.Direction == AttackDirection.Forward)
                return columnDelta == 0 && rowDelta >= 0f && rowDelta <= data.Range;

            return Mathf.Max(Mathf.Abs(rowDelta), columnDelta) <= data.Range;
        }

        private void LookAtTarget(Transform target)
        {
            var direction = target.transform.position - muzzle.position;
            direction.y = 0f;
            muzzle.rotation = Quaternion.LookRotation(direction);
        }

        private void Recoil()
        {
            muzzle.DOKill(true);
            muzzle.DOLocalMove(muzzle.localRotation * Vector3.back * RecoilDistance, RecoilDuration)
                .SetRelative()
                .SetEase(Ease.OutQuad)
                .SetLoops(2, LoopType.Yoyo);
        }
    }
}