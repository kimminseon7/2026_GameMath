using System.Linq;
using UnityEngine;

public class StandardDeviation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StandardDev();
    }

    // Update is called once per frame
    void StandardDev()
    {
        int n = 1000;
        float[] samples = new float[n];
        for (int i = 0; i < n; i++)
        {
            samples[i] = Random.Range(0f, 1f);
        }

        float mean = samples.Average();
        float sumOfSquares = samples.Sum(x => Mathf.Pow(x - mean, 2));
        float stdDev = Mathf.Sqrt(sumOfSquares / n);

        Debug.Log($"평균: {mean}, 표준편차: {stdDev}");
    }
}
