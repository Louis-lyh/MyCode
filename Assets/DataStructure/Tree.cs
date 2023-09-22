using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

namespace LouisCode.DataStructure
{ 
    public class Tree : MonoBehaviour
    {
        private void Start()
        {
            // 测试二分查找
            TestBinarySearch();

            // 测试二叉树
            TestBinarySearchTree();
        }
        
        // 测试二分查找
        private void TestBinarySearch()
        {
            var numbers = new int[]{1,2,3,4,5,6,7,8,9};    
            var index = BinarySearch(numbers,10);
            Debug.Log("index "+index);
        }

        // 测试二叉搜索树
        private void TestBinarySearchTree()
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(5);
            tree.Insert(2);
            tree.Insert(8);
            tree.Insert(3);
            tree.Insert(6);
            tree.Insert(4);
            tree.Insert(7);
            tree.Insert(1);
            tree.Insert(9);
            tree.Insert(0);
            tree.Insert(10);

            Debug.Log("树： "+tree.ToString());
            tree.RecursiveInset(-1);
            Debug.Log("树： "+tree.ToString());
             tree.RecursiveInset(15);
            Debug.Log("树： "+tree.ToString());
            // // 遍历
            // TestOrderTraversal(tree);

            // // 最大元素
            // Debug.Log("最大元素： "+tree.FindMax().Data);
            // // 最小元素
            // Debug.Log("最小元素： "+tree.FindMin().Data);
            // // 高度
            // Debug.Log("高度："+tree.GetHeight());
            // // 删除
            // tree.Delete(10);
            // // 中序
            // tree.InOrderTraversal();
            // // 删除
            // tree.Delete(2);
            //  // 中序
            // tree.InOrderTraversal();
            // 高度
            Debug.Log("高度："+tree.Height);
        }
        private void TestOrderTraversal(BinarySearchTree tree)
        {
            // 先序
            tree.PreOrderTraversal();
            // 中序
            tree.InOrderTraversal();
            // 后续
            tree.PostOrderTraversal();
            // 层序
            tree.LevelOrderTraversal();
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
}





