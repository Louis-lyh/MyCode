using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    private void Start() 
    {
        //乱序数组
        float[] numbers = new float[]{2,2.5f,3,3.5f,1,0,0.5f,1.1f,1.2f,1.3f,1.4f,1.5f,4.5f,1.6f,1.7f};
        //冒泡排序
        BubbleSort(numbers);
        //插入排序
        InsertionSort(numbers);
    }

    //冒泡排序
    private void BubbleSort(float[] numbers)
    {
        int count = 0;
        for(int i = 0; i < numbers.Length - 1; i++)
        {
            for(int j = i + 1; j < numbers.Length; j++ )
            {
                int number01 = (int) numbers[i];
                int number02 = (int) numbers[j];
                //前面大于后面 交换
                if(number01 > number02)
                {
                    float temp = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = temp;
                }
                //次数加一
                count++;
            }
        }

        Log("冒泡排序",numbers);
        Debug.Log($"lenght = {numbers.Length} ,count = {count} ,count / Lenght = {count / (float)numbers.Length}");
    }
    //插入排序
    private void InsertionSort(float[] numbers)
    {
        for(int i = 1; i < numbers.Length - 1; i++)
        {
            for(int j = i; j <  numbers.Length - 1; j--)
            {
                int number01 = (int) numbers[j - 1];
                int number02 = (int) numbers[j];
                //比前面的小交换
                if(number02 < number01)
                {
                    float temp = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = temp;
                }
                else //比前面的大直接插入下一个
                    break;
            }
        }

         Log("插入排序",numbers);
    }

    //打印
    private void Log(string sortName,float[] numbers)
    {
        string logStr = "";
        for(int i = 0;i < numbers.Length; i++)
        {
            logStr += ""+numbers[i];
            if(i < numbers.Length - 1)
                logStr += ",";
        }

        Debug.Log(sortName + " : " + logStr);
    }
}
