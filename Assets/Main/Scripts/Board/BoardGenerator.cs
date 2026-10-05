using Main.Scripts.Data;
using UnityEngine;

namespace Main.Scripts.Board
{
    public class BoardGenerator : MonoBehaviour
    {
        [SerializeField] private BoardConfig config;
        [SerializeField] private Cell cellPrefab;


        public int SpawnRow => config.Rows;

        private void Start()
        {
            Generate();
        }

        private void Generate()
        {
            var scale = Vector3.one * config.CellSize;

            for (var column = 0; column < config.Columns; column++)
            for (var row = 0; row < config.Rows; row++)
            {
                var cell = Instantiate(cellPrefab, GridToWorld(column, row), Quaternion.identity, transform);
                cell.Initialize(column, row, scale, row < config.PlaceableRowCount);
            }
        }

        public Vector3 GridToWorld(float column, float row)
        {
            var step = config.CellSize + config.Spacing;
            var width = config.Columns * config.CellSize + (config.Columns - 1) * config.Spacing;
            var height = config.Rows * config.CellSize + (config.Rows - 1) * config.Spacing;

            var firstCell = transform.position + new Vector3(
                (config.CellSize - width) * 0.5f,
                0f,
                (config.CellSize - height) * 0.5f);

            return firstCell + new Vector3(column * step, 0f, row * step);
        }


        public int GetRandomColumn()
        {
            return Random.Range(0, config.Columns);
        }
    }
}