using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LouisCode.DataStructure
{
    class LStack<T>
    {
        public LStack(int length)
        {
            // 初始化栈大小
            Datas = new T[length];
            // 初始化栈顶位置
            Top = -1;
        }
        // 栈
        private T[] Datas;
        // 长度
        public int Length => Datas.Length;
        
        // 栈顶位置
        public int Top;
        // 判断堆栈是否已满
        public bool IsFull()
        {
            return Top == Length - 1;
        }
        // 判断堆栈是否为空
        public bool IsEmpty()
        {
            return Top < 0;
        }
        // 入栈
        public void Push(T data)
        {
            if (IsFull())
            {
                Debug.LogError("栈满了");
                return;
            }
            Datas[++Top] = data;
        }
        // 出栈
        public T Pop()
        {
            if (IsEmpty())
            {
                Debug.LogError("栈空了");
                return default(T);
            }
            
            return  Datas[Top--];
        }
    }

}

