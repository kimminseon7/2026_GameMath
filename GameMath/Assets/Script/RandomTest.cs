using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class RandomTest : MonoBehaviour
{
    public TextMeshProUGUI[] Iabels = new TextMeshProUGUI[6];
    int[] counts = new int[6];

    public int trials = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Simulate()
    {
        for(int i = 0; i < trials; i++)
        {
            int result = Random.Range(1, 7);
            counts[result - 1]++;
        }

        for (int i = 0; i < counts.Length; i++)
        {
            float percent = (float)counts[i] / trials * 100f;
            string result = ($"{i+1}: {counts[i]}회({ percent:F3}%)");
            Iabels[i].text = result;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked()
    {
        for (int i = 0; i < 6;  i++)
        {
            counts[i] = 0;
        }
        Simulate();
    }
}
