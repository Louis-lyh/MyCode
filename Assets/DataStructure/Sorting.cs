using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    private void Start() 
    {
        //乱序数组
        float[] numbers = new float[]{2f,2.1f,2.5f,3,3.5f,1,0,0.5f,1.1f,1.2f,1.3f,1.4f,1.5f,4.5f,1.6f,1.7f,5.1f,4.1f,5.2f,4.2f,5.3f,4.3f,5.4f,4.4f};
        //冒泡排序
        float[] numbers01 = new float[numbers.Length];
        numbers.CopyTo(numbers01,0);
        BubbleSort(numbers01);
        //插入排序
        float[] numbers02 = new float[numbers.Length];
        numbers.CopyTo(numbers02,0);
        InsertionSort(numbers02);
        //快速排序
        float[] numbers03 = new float[numbers.Length];
        numbers.CopyTo(numbers03,0);
        QuickSort(numbers03,0,numbers03.Length - 1);
        //归并
        float[] numbers04 = new float[numbers.Length];
        numbers.CopyTo(numbers04,0);
        MergeSort(numbers04,0,numbers04.Length - 1);
        Log("归并",numbers04);
    }

    //冒泡排序
    private void BubbleSort(float[] numbers)
    {
        int count = 0;
        for(int i = 0; i < numbers.Length - 1; i++)
        {
            for(int j = 0; j < numbers.Length - 1 - i; j++ )
            {
                int number01 = (int) numbers[j];
                int number02 = (int) numbers[j + 1];
                //前面大于后面 交换
                if(number01 > number02)
                {
                    float temp = numbers[j + 1];
                    numbers[j + 1] = numbers[j];
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
        for(int i = 1; i < numbers.Length; i++)
        {
            for(int j = i; j > 0; j--)
            {
                int number01 = (int) numbers[j - 1];
                int number02 = (int) numbers[j];
                //比前面的小交换
                if(number02 < number01)
                {
                    float temp = numbers[j - 1];
                    numbers[j - 1] = numbers[j];
                    numbers[j] = temp;
                }
            }
        }

         Log("插入排序",numbers);
    }

    //快速排序
    private void QuickSort(float[] numbers,int startIndex,int endIndex)
    {
        if(startIndex >= endIndex)
            return;

        //完成一次排序
        var centerIndex = QuickSortUnit(numbers,startIndex,endIndex);
        //将左边进行排序
        QuickSort(numbers,startIndex,centerIndex - 1);
        //将右边进行排序
        QuickSort(numbers,centerIndex + 1,endIndex);
    }

    //快速排序
    private int QuickSortUnit(float[] numbers ,int startIndex,int endIndex)
    {
        float key = numbers[startIndex];
        while(endIndex > startIndex)
        {
            /*从后向前搜索比key小的值*/
            while((int)numbers[endIndex] >= (int)key && endIndex > startIndex)
                endIndex--;
            /*比key小的放左边*/
            numbers[startIndex] = numbers[endIndex];

            /*从前向后搜索比key大的值，比key大的放右边*/
            while((int)numbers[startIndex] <= (int)key && endIndex > startIndex)
                ++startIndex;

            /*比key大的放右边*/
            numbers[endIndex] = numbers[startIndex];
        }
        /*左边都比key小，右边都比key大。//将key放在游标当前位置。//此时startIndex等于endIndex */
        numbers[startIndex] = key;
        return endIndex;
    }

    //归并排序
    private void MergeSort(float[] numbers,int startIndex,int endIndex)
    {
        if(startIndex < endIndex)
        {
            int mid = (startIndex + endIndex) / 2;
            MergeSort(numbers,startIndex,mid);
            MergeSort(numbers,mid + 1,endIndex);
            MergeSortUnit(numbers, startIndex, mid, endIndex);
        }
    }
    private void MergeSortUnit(float[] numnbers,int startIndex,int mid,int endIndex)
    {
        float[] temp = new float[endIndex - startIndex + 1];
        int m = startIndex;
        int n = mid + 1;
        int k = 0;

        while(n <= endIndex && m <= mid)
        {
            if(numnbers[m] > numnbers[n])
                temp[k++] = numnbers[n++];
            else
                temp[k++] = numnbers[m++];
        }
        while(n < endIndex + 1)
            temp[k++] = numnbers[n++];
        while (m < mid + 1) 
            temp[k++] = numnbers[m++];
        for (k = 0, m = startIndex; m < endIndex + 1; k++, m++) 
            numnbers[m] = temp[k];
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
