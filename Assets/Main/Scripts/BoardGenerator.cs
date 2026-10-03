using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts
{
    public class BoardGenerator : MonoBehaviour
    {
        [SerializeField] private BoardConfig config;
        Cell CellPrefab => Resources.Load<Cell>("Props/Cell");
        
        public int Columns => config.Columns;
        public int Rows => config.Rows;
        
        private List<Cell> topCells = new List<Cell>();

        private void Start()
        {
            Generate();
        }

        void Generate()
        {
            float step = config.CellSize + config.Spacing;
            float width = config.Columns * config.CellSize + (config.Columns - 1) * config.Spacing;
            float height = config.Rows * config.CellSize + (config.Rows - 1) * config.Spacing;
 
            Vector3 firstCellPosition = transform.position + new Vector3(
                (config.CellSize - width) * 0.5f,
                0f,
                (config.CellSize - height) * 0.5f);
            
            topCells.Add(CellPrefab);
 
            for (int column = 0; column < config.Columns; column++)
            {
                for (int row = 0; row < config.Rows; row++)
                {
                    Vector3 position = firstCellPosition + new Vector3(column * step, 0f, row * step);
                    Vector3 scale = Vector3.one * config.CellSize;
                    var cell = Instantiate(CellPrefab, position, Quaternion.identity, transform);
                    cell.Initialize(column, row, scale, row < config.Rows - config.PlaceableRowCount);

                    if (row + 1 == config.Rows)
                        topCells.Add(cell);
                }
            }
        }
        
        public Vector3 GridToWorld(float column, float row)
        {
            float step = config.CellSize + config.Spacing;
            float width = config.Columns * config.CellSize + (config.Columns - 1) * config.Spacing;
            float height = config.Rows * config.CellSize + (config.Rows - 1) * config.Spacing;

            Vector3 firstCell = transform.position + new Vector3(
                (config.CellSize - width) * 0.5f,
                0f,
                (config.CellSize - height) * 0.5f);

            return firstCell + new Vector3(column * step, 0f, row * step);
        }
        
        public Vector3 GetRandomTopCellPosition() => topCells[UnityEngine.Random.Range(0, topCells.Count)].Position;
    }
}
