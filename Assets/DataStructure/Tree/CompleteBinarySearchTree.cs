using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace LouisCode.DataStructure
{
    // 完全二叉搜索树
    public class CompleteBinarySearchTree
    {
        // 元素
        private int[] _items;
        public int Length => _items.Length;
        public CompleteBinarySearchTree(int[] items)
        {
            _items = items;
            ArrangeItem();
        }
        
        // 获取左子树的数量
        public int GetLeftLength(int root)
        {
            // 左儿子
            var leftChild = root * 2 + 1;

            if(leftChild >= Length)
                return 0;
            var length = 0;
            // 左儿子长度
            length =  GetRootLength(leftChild);
            
            return length;
        }
        // 获得子节点个数
        public float GetLeftChildCount(int root)
        {
            // 左节点
            var leftRoot = root * 2 + 1;
            
            // 树中完美二叉树的层数
            var storey =  (int)Mathf.Log(Length + 1,2);
            // （没用填满的那一层的个数） 
            var residue = Length -  (int)(Mathf.Pow(2, storey) - 1);
            
            // 当前节点所在的层
            var rootStorey =  (int)Mathf.Ceil(Mathf.Log(leftRoot + 1 + 1,2 ));
            // 当前节点的高
            var height = storey - rootStorey + 1;
            
            // 节点个数
            var itemCount = (int)Mathf.Pow(2, height) - 1;
            // 加上没有填满的那一层
            itemCount += (int)Mathf.Min(residue, Mathf.Pow(2, height + 1 - 1));
            

            return itemCount;
        }

        // 获得节点长度
        private int GetRootLength(int root)
        {
            var length = 0;
            if(root >= Length)
                return length;
            
            length = 1;
            
            // 左儿子长度
            var leftChild = root * 2 + 1;
            length +=  GetRootLength(leftChild);
            
            // 有儿子长度
            var rightChild = root * 2 + 2;
            length +=  GetRootLength(rightChild);
            
            return length;
        }
        // 整理
        private void ArrangeItem()
        {
            // 先排序item
            var items = BubbleSort();
            // 整理
            ArrangeItem(0, _items.Length - 1, 0,items);
        }
        // 
        private void ArrangeItem(int leftIndex,int rightIndex,int root,int[] sortItem)
        {
            // 超过范围退出
            if(root >= Length)
                return;
            
            // 左子树数量
            var count = GetLeftLength(root);

            // 把左边第count个元素放入root
            _items[root] = sortItem[leftIndex + count];
            // 左边
            ArrangeItem(leftIndex, leftIndex + count - 1, root * 2 + 1, sortItem);
            // 右边
            ArrangeItem(leftIndex + count + 1,rightIndex, root * 2 + 2, sortItem);
        }

        // 冒泡排序
        private int[] BubbleSort()
        {
            //
            var items = new int[_items.Length];
            // 复制一份
            for (int i = 0; i < _items.Length; i++)
            {
                items[i] = _items[i];
            }
            
            for (int i = 0; i < items.Length - 1; i++)
            {
                for (int j = 0; j < items.Length - 1 - i; j++)
                {
                    if (items[j] > items[j + 1])
                    {
                        var temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }

            return items;
        }
        // 字符串格式化
        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < _items.Length; i++)
            {
                str += _items[i];
                if (i < _items.Length - 1)
                    str += "";
            }
            
            return str;
        }
    }
}


