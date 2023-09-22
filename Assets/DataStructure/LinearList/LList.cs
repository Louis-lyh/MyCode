using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LouisCode.DataStructure
{
    /// <summary>
    /// 链表节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class LNode<T>
    {
        public T Data;
        public LNode<T> Next;
    }
    /// <summary>
    /// 链表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class LList<T>
    {
        // 头节点
        public LNode<T> Header;
        
        /// <summary>
        /// 获取长度
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            LNode<T> temp = Header;
            int length = 0;
            // 遍历节点
            while(temp != null)
            {
                temp = temp.Next;
                length++;
            }
            
            return length;
        }

        /// <summary>
        /// 通过索引获取节点
        /// </summary>
        /// <param name="index">节点索引</param>
        /// <returns></returns>
        public LNode<T> FindIndex(int index)
        {
            LNode<T> temp = Header;
            int i = 0;
            while(temp != null && i < index)
            {
                temp = temp.Next;
                i++;
            }
            if(i == index)
                return temp;

            return null;
        }

        /// <summary>
        /// 通过值获取节点
        /// </summary>
        /// <param name="data">节点的值</param>
        /// <returns></returns>
        public LNode<T> FindValue(T data)
        {
            LNode<T> temp = Header;
            // 循环遍历节点找到对应值的节点
            while(temp != null && !temp.Data.Equals(data))
                temp = temp.Next;
            
            return temp;
        }
        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="data">节点数据</param>
        /// <param name="index">插入位置</param>
        /// <returns></returns>
        public LNode<T> Insert(T data,int index)
        {
            // 创建新节点
            LNode<T> temp = new LNode<T>();
            temp.Data = data;
            // 插入到第一个
            if(index == 0)
            {   
                temp.Next = Header;
                Header = temp;
                return Header;
            }
            // 找到前一个节点
            LNode<T> front = FindIndex(index - 1);

            // 没有前一个节点 数据错误
            if(front == null)
            {
                Debug.LogError($"参数错{index}");
                return front;
            }
            else
            {
                // 找到前一个节点进行插入
                temp.Next = front.Next;
                front.Next = temp;
                return Header;
            }
        }
        
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="index">节点索引</param>
        /// <returns></returns>
        public LNode<T> Delete(int index)
        {
            // 删除头节点
            if(index == 0)
            {
                Header = Header.Next;
                return Header;
            }
            else
            {
                // 找到前一个节点
                LNode<T> temp = FindIndex(index - 1);
                // 前一个节点为空
                if(temp == null)
                {
                    Debug.LogError($"参数错：{index}");
                    return Header;
                }
                // 节点为空
                if(temp.Next == null)
                {
                    Debug.LogError($"参数错{index}");
                    return Header;
                }
                // 删除节点
                LNode<T> oldNode = temp.Next;
                temp.Next = oldNode.Next;

                return Header;
            }
        }
        // 格式化输出
        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < Length(); i++)
            {
                var node = FindIndex(i);
                str += $"{i}:{node.Data}\n";
            }
            return str;
        }
    }

}

