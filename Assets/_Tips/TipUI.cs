using TMPro;
using UnityEngine;

public class TipUI : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public TipData[] tips;

    public void ShowRandomTip()
    {
        int randomIndex = Random.Range(0, tips.Length);
        string[] tipLines = tips[randomIndex].tipsLines;
        string message = string.Join("\n", tipLines);
        tipText.text = message;
    }
}
