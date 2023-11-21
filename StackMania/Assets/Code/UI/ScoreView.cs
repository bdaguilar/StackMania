using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _text;

    public void Reset()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int newScore)
    {
        _text.text = newScore.ToString();
    }
}

