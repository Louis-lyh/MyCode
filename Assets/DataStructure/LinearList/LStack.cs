using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LouisCode.DataStructure
{
    // 链式存储堆栈
    class ListStack<T>
    {
        /// <summary>
        /// 堆栈元素节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class SNode<T>
        {
            public T Data;
            public SNode<T> Next;
        }
        
        public ListStack()
        {
            _topNode = new SNode<T>();
        }

        // 栈顶节点
        private SNode<T> _topNode;
    
        /// <summary>
        /// 元素数量
        /// </summary>
        public int Count => GetCount();
        private int GetCount()
        {
            var temp = _topNode.Next;
            var length = 0;
            // 循环遍历栈元素
            while (temp != null)
            {
                length++;
                temp = temp.Next;
            }

            return length;
        }

        /// <summary>
        /// 判断堆栈是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return _topNode.Next == null;
        }
        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="data"></param>
        public void Push(T data)
        {
            // 创建新节点
            SNode<T> newNode = new SNode<T>();
            newNode.Data = data;
            // 插入
            newNode.Next = _topNode.Next;
            _topNode.Next = newNode;
        }
    
        /// <summary>
        ///  出栈
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            // 栈为空
            if (IsEmpty())
            {
                Debug.LogError("栈空了");
                return default;
            }
            
            // 移除第一个元素
            var temp = _topNode.Next;
            _topNode.Next = temp.Next;

            return temp.Data;
        }
    }
}


