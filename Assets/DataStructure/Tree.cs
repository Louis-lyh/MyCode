using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{


    private void Start() 
    {
        var numbers = new int[]{1,2,3,4,5,6,7,8,9};    
        var index = BinarySearch(numbers,10);
        Debug.Log("index "+index);
    }

    // 二分查找
    private int BinarySearch(int[] numbers,int value)
    {
        int left , right , mid;
        // 初始化
        left = 0;
        right = numbers.Length - 1;
        while(left <= right)
        {
            // 中间索引
            mid = (left + right) / 2;
            var midValue = numbers[mid];
            // 中间值大于目标值 右指针左移
            if(midValue > value) right = mid - 1;
            // 中间值小于目标值 左指针有移
            else if(midValue < value) left = mid + 1;
            // 中间值等于目标值 返回索引
            else
                return mid;
        }

        return -1;
    }

}
