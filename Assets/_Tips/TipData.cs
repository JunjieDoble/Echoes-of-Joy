using UnityEngine;

[CreateAssetMenu(fileName = "New Tip Data", menuName = "Tip/Tip Data")]
public class TipData : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] tipsLines;
}