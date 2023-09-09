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
        tree.RecursiveInset(5);
        tree.RecursiveInset(2);
        tree.RecursiveInset(8);
        tree.RecursiveInset(3);
        tree.RecursiveInset(6);
        tree.RecursiveInset(4);
        tree.RecursiveInset(7);
        tree.RecursiveInset(1);
        tree.RecursiveInset(9);
        tree.RecursiveInset(0);
        tree.RecursiveInset(10);
        
        // 遍历
        TestOrderTraversal(tree);

        // 最大元素
        Debug.Log("最大元素： "+tree.FindMax().Data);
        // 最小元素
        Debug.Log("最小元素： "+tree.FindMin().Data);
        // 高度
        Debug.Log("高度："+tree.GetHeight());
        // 删除
        tree.Delete(10);
        // 中序
        tree.InOrderTraversal();
        // 删除
        tree.Delete(2);
         // 中序
        tree.InOrderTraversal();
        // 高度
        Debug.Log("高度："+tree.GetHeight());
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
        return GetHeight(RootNode);
    }
    private int GetHeight(TreeNode node)
    {
        if(node == null)
            return 0;

        var leftHeight = GetHeight(node.Left) + 1;
        var rightHeight = GetHeight(node.Right) + 1;

        return Mathf.Max(leftHeight,rightHeight);
    }
    
    // 查找元素返回其所在节点的地址
    public TreeNode Find(int data)
    {
        var temp = RootNode;
        while(temp != null)
        {
            // 找到
            if(temp.Data == data)
                return temp;
            // 在坐标
            if(temp.Data > data)
                temp = temp.Left;
            // 在右边
            else
                temp = temp.Right;
        }
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
    // 递归插入
    public void RecursiveInset(int data)
    {
        RootNode = RecursiveInset(data,RootNode);
    }
    private TreeNode RecursiveInset(int data,TreeNode node)
    {
        if(node == null)
        {
            TreeNode newNode = new TreeNode(data);
            node = newNode;
            return node;
        }
        else
        {
            if(data <= node.Data)
            {
                node.Left = RecursiveInset(data,node.Left);
            }
            else
            {
                node.Right = RecursiveInset(data,node.Right);
            }
            return node;
        }
    }

    // 删除
    public void Delete(int data)
    {
        RootNode = Delete(data,RootNode);
    }
    private TreeNode Delete(int data,TreeNode node)
    {
        if(node == null)
        {
            Debug.LogError("没有该元素 "+data);
            return null;
        }
        else
        {
            // 找到
            if(node.Data == data)
            {
                // 删除的节点两个子节点都在
                if(node.Left != null && node.Right != null)
                {   
                    // 找到右边最大
                    var temp = FindMax(node.Left);
                    // 替换
                    node.Data = temp.Data;
                    // 删除
                    node.Left = Delete(node.Data,node.Left);                    
                }
                // 没有
                else if(node.Left != null)
                    node = node.Left;
                else if(node.Right != null)
                    node = node.Right;

                return node;
            }
            // 小于节点往左边走
            else if(data < node.Data)
            {
                node.Left = Delete(data,node.Left);
            }
            // 大于节点往右边走
            else if(data > node.Data)
            {
                node.Right = Delete(data,node.Right);
            }

            return node;
        }
    }

    // 查找最小元素
    public TreeNode FindMin()
    {
        return FindMin(RootNode);
    }
    private TreeNode FindMin(TreeNode node)
    {
        // 为空退出
        if(IsEmpty())
            return null;

        var temp = node;
        
        // 找到最左边的节点
        while(temp != null)
        {
            // 左边为空 返回
            if(temp.Left == null)
                return temp;
            else // 左边不为空继续循环
                temp = temp.Left;
        }

        return null;
    }
    // 查找最大元素
    public TreeNode FindMax()
    {
        return FindMax(RootNode);
    }
    private TreeNode FindMax(TreeNode node)
    {
        // 为空退出
        if(IsEmpty())
            return null;
        
        var temp = node;

        // 找到最右边的节点
        while(temp != null)
        {
            // 右边为空 返回
            if(temp.Right == null)
                return temp;
            else // 右边不为空继续循环
                temp = temp.Right;
        }

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
        Debug.Log("后序遍历:");
        // 为空退出
        if(IsEmpty())
            return;

        // 创建队列
        LQueue<TreeNode> queue = new LQueue<TreeNode>(10);
        queue.AddQueue(RootNode);

        while(!queue.IsEmptry())
        {
            // 拿取一个
            var temp = queue.Delete();
            // 打印
            Debug.Log(""+temp.Data);
            // 左边放入队列
            if(temp.Left != null)
                queue.AddQueue(temp.Left);
            // 右边放入队列
            if(temp.Right != null)
                queue.AddQueue(temp.Right);
        }

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


