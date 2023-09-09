using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

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
        
        // 先序
        tree.PreOrderTraversal();
        // 中序
        tree.InOrderTraversal();
        // 后续
        tree.PostOrderTraversal();
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
// 树节点
class TreeNode
{
    public TreeNode(int data)
    {
        Data = data;
    }

    public int Data;
    public TreeNode Left;
    public TreeNode Right;
}
// 二叉搜索树
class BinarySearchTree
{
    // 根节点
    public TreeNode RootNode;
    
    // 判断是否为空
    public bool IsEmpty()
    {
        return RootNode == null;
    }
    
    // 获取高度
    public int GetHeight()
    {
        return 0;
    }
    
    // 查找元素返回其所在节点的地址
    public TreeNode Find(int data)
    {
        return null;
    }
    
    // 插入
    public void Insert(int data)
    {
        // 新建节点
        TreeNode newNode = new TreeNode(data);

        if (RootNode == null)
            RootNode = newNode;
        else
        {
            var temp = RootNode;
            while (temp != null)
            {
                // 左边
                if (newNode.Data <= temp.Data)
                {
                    // 放入左边
                    if (temp.Left == null)
                    {
                        temp.Left = newNode;
                        return;
                    }
                    else // 往左边走
                    {
                        temp = temp.Left;
                        continue;
                    }
                }
                // 右边
                else
                {
                    // 放入右边
                    if (temp.Right == null)
                    {
                        temp.Right = newNode;
                        return;
                    }
                    else// 往右边走
                    {
                        temp = temp.Right;
                        continue;
                    }
                }
            }
        }

    }
    
    // 删除
    public TreeNode Delete(int data)
    {
        return null;
    }

    // 查找最小元素
    public TreeNode FindMin()
    {
        return null;
    }
    // 查找最大元素
    public TreeNode FindMax()
    {
        return null;
    }
    
    // 先序遍历
    public void PreOrderTraversal()
    {
        Debug.Log("先序遍历:");
        RecursiveTraversal(RootNode, 0);
    }
    
    // 中序遍历
    public void InOrderTraversal()
    {
        Debug.Log("中序遍历:");
        RecursiveTraversal(RootNode, 1);
    }
    
    // 后序遍历
    public void PostOrderTraversal()
    {
        Debug.Log("后序遍历:");
        RecursiveTraversal(RootNode, 2);
    }
    
    // 层次遍历
    public void LevelOrderTraversal()
    {
        
    }
    // 递归遍历
    private void RecursiveTraversal(TreeNode treeNode,int orderType)
    {
        if (treeNode != null) 
        {
            // 先序遍历输出
            if(orderType == 0)
                Debug.Log(treeNode.Data + " ");
           
            // 递归左子树
            RecursiveTraversal(treeNode.Left,orderType);
            
            // 中序遍历输出
            if(orderType == 1)
                Debug.Log(treeNode.Data + " ");
            
            // 递归右子树
            RecursiveTraversal(treeNode.Right,orderType);
            
            // 后序遍历输出
            if(orderType == 2)
                Debug.Log(treeNode.Data + " ");
        }
    }
}


