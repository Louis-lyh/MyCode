using System.Collections.Generic;
using UnityEngine;

namespace LouisCode.DataStructure
{
    public class LinearList : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // 测试链表
            //TestList();
            
            // 测试栈
           // TestLStack();

            // 测试队列
            TestLQueue();

        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        // 测试链表
        private void TestList()
        {
            // 链表
            LList<int> lNodeList = new LList<int>();
            
            // 初始化链表
            for (int i = 0; i < 10; i++)
            {
                // 插入
                lNodeList.Insert(i + 1, i);
            }
            
            // 获取长度
            var length = lNodeList.Length();
            Debug.Log("length "+length);
            
            // 删除
            lNodeList.Delete(0);
            lNodeList.Delete(3);
            lNodeList.Delete(lNodeList.Length() - 1);
            // 打印
            Debug.Log("lNodeList " + lNodeList.ToString()); 
            
            // 添加
            lNodeList.Insert(1, 0);
            
            lNodeList.Insert(5, 4);
            
            lNodeList.Insert(10, 9);
            // 打印
            Debug.Log("lNodeList " + lNodeList.ToString()); 
        }
        
        // 测试栈
        private void TestLStack()
        {
            // 数组存储的栈
            LStack<int> lStack = new LStack<int>(10);
            
            lStack.Push(1);
            lStack.Push(2);
            Debug.Log("lStack "+lStack.Pop());
            lStack.Push(3);
            Debug.Log("lStack "+lStack.Pop()); 
            for (int i = 0; i < lStack.Length; i++)
            {
                lStack.Push(i);
            }

            for (int i = 0; i < lStack.Length; i++)
            {
                Debug.Log("lStack "+lStack.Pop());
            }
            
            // 链式存储的栈
            ListStack<int> lNodeListStack = new ListStack<int>();
            
            lNodeListStack.Push(1);
            lNodeListStack.Push(2);
            Debug.Log("lNodeListStack "+lNodeListStack.Pop());
            lNodeListStack.Push(3);
            Debug.Log("lNodeListStack "+lNodeListStack.Pop()); 
            for (int i = 0; i < 10; i++)
            {
                lNodeListStack.Push(i);
            }

            for (int i = 0; i < 10; i++)
            {
                Debug.Log("lNodeListStack "+lNodeListStack.Pop());
            }
            
        }
        
        // 测试队列
        private void TestLQueue()
        {
            ListQueue<int> queue = new ListQueue<int>();

            int value = 1;
            while(value < 10)
            {
                queue.Enqueue(value);
                value++;
                Debug.Log("queue.Lenght "+queue.Length);
            }

            queue.Enqueue(value);
            Debug.Log("queue :  " + queue.ToString());
            value = queue.Dequeue();
            Debug.Log("value :  " + value);
            Debug.Log("queue :  " + queue.ToString());
            value = queue.Dequeue();
            Debug.Log("value :  " + value);

            Debug.Log("queue :  " + queue.ToString());

            queue.Enqueue(value);
            Debug.Log("queue.Lenght "+queue.Length);
            Debug.Log("queue :  " + queue.ToString());


            value = queue.Dequeue();
            value = queue.Dequeue();

            queue.Enqueue(value);
            Debug.Log("queue.Lenght "+queue.Length);
            Debug.Log("queue :  " + queue.ToString());


            value = queue.Dequeue();
            value = queue.Dequeue();

            queue.Enqueue(value);
            Debug.Log("queue.Lenght "+queue.Length);
            Debug.Log("queue :  " + queue.ToString());

        }
    }
}
