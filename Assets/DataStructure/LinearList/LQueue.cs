using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LouisCode.DataStructure
{
    // 链式存储队列
    class ListQueue<T>
    {
        // 队列节点
        private class QNode<T>
        {
            public T Data;
            public QNode<T> Next;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public ListQueue()
        {
            _front = new QNode<T>();
            _after = new QNode<T>();
        }
    
        // 数据
        QNode<T> _front;
        // 尾节点
        QNode<T> _after;
    
        /// <summary>
        /// 队列长度
        /// </summary>
        public int Length => GetLength();
        private int GetLength()
        {
            var temp = _front.Next;
            int i = 0;
            while(temp != null)
            {
                temp = temp.Next;
                i++;
            }
    
            return i;
        }
    
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmptry()
        {
            return GetLength() == 0;
        }
    
        /// <summary>
        /// 加入队列
        /// </summary>
        /// <param name="data"></param>
        public void Enqueue(T data)
        {
           // 新建
           QNode<T> newNode = new QNode<T>();
           newNode.Data = data;
    
            // 头节点为空
            if(_front.Next == null)
                _front.Next = newNode;
    
            // 尾节点为空
            if(_after.Next == null)
                _after.Next = newNode;
            else
            {
                _after.Next.Next = newNode;
                _after.Next = newNode;
            }
        }
        /// <summary>
        ///  出队列
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            
           if(IsEmptry())
           {
               Debug.LogError("队列为空");
               return default;
           }
           
           // 移除节点
           var temp = _front.Next;
           _front.Next = temp.Next;
    
            return temp.Data;
        }
        /// <summary>
        /// 格式化文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var temp = _front.Next;
            string str = "";
            while(temp != null)
            {
                str += temp.Data + " , ";
                temp = temp.Next;
            }
    
            return str;
        }
    
    }
}


