using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    private void Start() 
    {
        float[] numbers = new float[]{2,2.5f,3,3.5f,1,1.5f,0,0.5f};
        BubbleSort(numbers);
        Log(numbers);
    }

    //冒泡排序
    private void BubbleSort(float[] numbers)
    {
        for(int i = 0; i < numbers.Length - 1; i++)
        {
            for(int j = i + 1; j < numbers.Length; j++ )
            {
                int number01 = (int) numbers[i];
                int number02 = (int) numbers[j];
                if(number01 > number02)
                {
                    float temp = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = temp;
                }
            }
        }
    }

    //打印
    private void Log(float[] numbers)
    {
        string logStr = "";
        for(int i = 0;i < numbers.Length; i++)
        {
            logStr += ""+numbers[i];
            if(i < numbers.Length - 1)
                logStr += ",";
        }
        Debug.Log(logStr);
    }
}
