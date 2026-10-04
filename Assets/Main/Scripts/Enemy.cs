using System;
using UnityEngine;

namespace Main.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private EnemyData data;
        private BoardGenerator board;
    
        public void Init(EnemyData data,Vector3 spawnPosition)
        {
            this.data = data;
            //this.board = board;
            transform.position = spawnPosition;
        }

        // private void Update()
        // {
        //     Move();
        //
        //     if (Row <= BaseRow)
        //     {
        //         enabled = false;
        //         EventBus.OnEnemyReachedBase?.Invoke(this);
        //     }
        // }
        //
        // private void Move()
        // {
        //     Row -= data.Speed * Time.deltaTime;
        //     transform.position = board.GridToWorld(Column, Row);
        // }
    }
}
