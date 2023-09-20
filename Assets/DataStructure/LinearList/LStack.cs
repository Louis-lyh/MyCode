using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LouisCode.DataStructure
{
    // 链式存储堆栈
    class ListStack
    {
        public ListStack()
        {
            Datas = new List();
            Datas.Header = new LNode();
        }

        // 栈
        public List Datas;
    
        // 长度
        public int Length => Datas.Length();
        // 判断堆栈是否为空
        public bool IsEmpty()
        {
            return Datas.Header.Next == null;
        }
        // 入栈
        public void Push(int data)
        {
            LNode newNode = new LNode();
            newNode.Data = data;
            newNode.Next = Datas.Header.Next;
            Datas.Header.Next = newNode;
        }
    
        // 出栈
        public int Pop()
        {
            if (Datas.Header.Next == null)
            {
                Debug.LogError("栈空了");
                return -1;
            }

            var temp = Datas.Header.Next;
            Datas.Header.Next = temp.Next;

            return temp.Data;
        }
    }
}


