using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GachaManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    void Start()
    {
        // Inspector 연결 안 했을 경우 자동으로 찾아서 연결
        if (resultText == null)
        {
            resultText = FindObjectOfType<TextMeshProUGUI>();
        }
    }

    public void SimulateGachaSingle()
    {
        string result = Simulate();

        Debug.Log("Gacha Result: " + result);

        if (resultText != null)
            resultText.text = "Gacha Result: " + result;
    }

    public void SimulateGachaTenTime()
    {
        List<string> results = new List<string>();

        for (int i = 0; i < 9; i++)
        {
            results.Add(Simulate());
        }

        float r2 = Random.value;
        string result2 = (r2 < 2f / 3f) ? "A" : "S";
        results.Add(result2);

        string finalResult = string.Join(", ", results);

        Debug.Log("Gacha Result: " + finalResult);

        if (resultText != null)
            resultText.text = "Gacha Result: " + finalResult;
    }

    string Simulate()
    {
        float r = Random.value;

        if (r < 0.4f) return "C";
        else if (r < 0.3f) return "B";
        else if (r < 0.2f) return "A";
        else return "S";
    }
}