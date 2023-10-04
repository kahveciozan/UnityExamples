using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bu fonsiyonu neden farklý bir classta yaptý anlayamadý. Ben olsam ayný classta yapardim. 04.10.23- Ozan
/// </summary>

public class CallPieChart : MonoBehaviour
{
    private int chartCount;

    private void Start()
    {
        chartCount = GetComponent<PieChart>().imagesPieChart.Length;
    }

    public void GenerateRandomPieChart()
    {
        float[] values = new float[chartCount];

        for(int i = 0; i < values.Length; i++)
        {
            values[i] = Random.Range(0.0f, 100.0f);
        }

        GetComponent<PieChart>().SetValues(values);
    }
}
