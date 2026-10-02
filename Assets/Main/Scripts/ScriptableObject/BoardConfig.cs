using UnityEngine;

[CreateAssetMenu(fileName = "BoardConfig", menuName = "Board Defence/Board Config")]
public class BoardConfig : ScriptableObject
{
    [SerializeField, Min(1)] private int columns = 4;
    [SerializeField, Min(1)] private int rows = 8;
 
    [SerializeField, Min(0)] private int placeableRowCount = 4;
 
    [SerializeField, Min(0.01f)] private float cellSize = 1f;
 
    [SerializeField, Min(0f)] private float spacing = 0.1f;
 
    public int Columns => columns;
    public int Rows => rows;
    public int PlaceableRowCount => placeableRowCount;
    public float CellSize => cellSize;
    public float Spacing => spacing;
 
    private void OnValidate()
    {
        placeableRowCount = Mathf.Clamp(placeableRowCount, 0, rows);
    }
}
