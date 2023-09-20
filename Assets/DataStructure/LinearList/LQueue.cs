using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LouisCode.DataStructure
{
    // 链式存储队列
    class LQueue
    {
    
        public LQueue()
        {
            _front = new LNode();
            _after = new LNode();
        }
    
        // 数据
        LNode _front;
        // 尾节点
        LNode _after;
    
        // 长度
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
    
        // 是否为空
        public bool IsEmptry()
        {
            return GetLength() == 0;
        }
    
        // 添加
        public void AddQueue(int data)
        {
           // 新建
           LNode newNode = new LNode();
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
        // 删除
        public int Delete()
        {
           if(_front.Next == null)
           {
               Debug.LogError("队列为空");
               return -1;
           }
    
           var temp = _front.Next;
           _front.Next = temp.Next;
    
            return temp.Data;
        }
    
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


