using UnityEngine;

[CreateAssetMenu(fileName = "CellColorConfig", menuName = "Board Defence/Cell Color Config")]
public class CellColorConfig : ScriptableObject
{
    [SerializeField] private Color emptyColor = new Color(0.55f, 0.75f, 0.55f);
    [SerializeField] private Color blockedColor = new Color(0.4f, 0.4f, 0.45f);
    [SerializeField] private Color fillableColor = new Color(0.3f, 0.9f, 0.3f);
    [SerializeField] private Color filledColor = new Color(0.9f, 0.3f, 0.3f);

    public Color EmptyColor => emptyColor;
    public Color BlockedColor => blockedColor;
    public Color FillableColor => fillableColor;
    public Color FilledColor => filledColor;
}

