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
    }
}
