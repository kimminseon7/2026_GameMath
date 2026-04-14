using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributionTest : MonoBehaviour
{
    int PoissonDistribution(float Iambda)
    {
        int k = 0;
        float p = 1f;
        float L = Mathf.Exp(-Iambda);
        while (p > L)
        {
            k++;
            p *= Random.value;
        }
        return k - 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            int count = PoissonDistribution(3f);
            Debug.Log($"Minute {i + 1}: {count} events ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
