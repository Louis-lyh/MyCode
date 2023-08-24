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
        Log("快速排序",numbers03);
        //归并
        float[] numbers04 = new float[numbers.Length];
        numbers.CopyTo(numbers04,0);
        MergeSort(numbers04,0,numbers04.Length - 1);
        Log("归并",numbers04);
        //堆排序
        float[] numbers05 = new float[numbers.Length];
        numbers.CopyTo(numbers05,0);
        Heap_sort(numbers05);
        Log("堆排序",numbers05);
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
            //中间缩影
            int mid = (startIndex + endIndex) / 2;
            //排序左边
            MergeSort(numbers,startIndex,mid);
            //排序右边
            MergeSort(numbers,mid + 1,endIndex);
            //有序合并
            MergeSortUnit(numbers, startIndex, mid, endIndex);
        }
    }
    //归并排序
    private void MergeSortUnit(float[] numbers,int startIndex,int mid,int endIndex)
    {
        //存储合并后的数组
        float[] temp = new float[endIndex - startIndex + 1];
        //左边数组的开始
        int m = startIndex;
        //右边数组的开始
        int n = mid + 1;
        //融合后数组的索引
        int k = 0;
        
        //合并
        while(n <= endIndex && m <= mid)
        {
            //先将小的数放入合并数组
            if(numbers[m] > numbers[n])
                temp[k++] = numbers[n++];
            else
                temp[k++] = numbers[m++];
        }
        //将右边数组没放完的数依次放入合并数组
        while(n < endIndex + 1)
            temp[k++] = numbers[n++];
        //将左边数组没放完的数一次放入合并数组
        while (m < mid + 1) 
            temp[k++] = numbers[m++];
        
        //将合并数组依次放入远数组
        for (k = 0, m = startIndex; m < endIndex + 1; k++, m++) 
            numbers[m] = temp[k];
    }

    //堆排序
    private void Heap_sort(float[] numbers)
    {
        int length = numbers.Length;
        // 初始化，i从最后一个父节点开始挑战
        for (int i = length / 2 - 1; i >= 0; i--)
        {
            Max_Heapify(numbers,i,length - 1);
        }
        // 先将第一个元素和已经排好的元素前一位交换，在重新调整，直到排序完毕
        for (int i = length - 1; i > 0; i--)
        {
            float temp = numbers[0];
            numbers[0] = numbers[i];
            numbers[i] = temp;

            Max_Heapify(numbers,0,i - 1);
        }
    }
    //最大堆
    private void Max_Heapify(float[] numbers, int start, int end)
    {
        // 建立父节点指标和子节点指标
        int dad = start;
        int son = dad * 2 + 1;
        
        //若子节点指标在范围内比较
        while(son <= end)
        {
            //先比较两个子节点大小
            if (son + 1 <= end && numbers[son] < numbers[son + 1])
            {
                son++;
            }
            // 如果父节点大于子节点代表调整完毕，直接跳出函数
            if(numbers[dad] > numbers[son])
                return;
            else
            {
                float temp = numbers[dad];
                numbers[dad] = numbers[son];
                numbers[son] = temp;

                dad = son;
                son = dad * 2 + 1;
            }
        }
        

    }
    //交换
    private void Swap(ref float a,ref float b)
    {
        float temp = a;
        a = b;
        b = temp;
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
