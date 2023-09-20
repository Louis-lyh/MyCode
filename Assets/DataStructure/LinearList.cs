using System.Collections;
using System.Collections.Generic;
using System.Data;
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
            List lNodeList = new List();
            
            // 初始化链表
            for (int i = 0; i < 10; i++)
            {
                LNode node = new LNode();
                node.Data = i + 1;
                lNodeList.Insert(node, i);
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
            LNode newNode = new LNode();
            newNode.Data = 1;
            lNodeList.Insert(newNode, 0);
            
            LNode newNode02 = new LNode();
            newNode02.Data = 5;
            lNodeList.Insert(newNode02, 4);
            
            LNode newNode03 = new LNode();
            newNode03.Data = 10;
            lNodeList.Insert(newNode03, 9);
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
            ListStack lNodeListStack = new ListStack();
            
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
            LQueue queue = new LQueue();

            int value = 1;
            while(value < 10)
            {
                queue.AddQueue(value);
                value++;
                Debug.Log("queue.Lenght "+queue.Length);
            }

            queue.AddQueue(value);
            Debug.Log("queue :  " + queue.ToString());
            value = queue.Delete();
            Debug.Log("value :  " + value);
            Debug.Log("queue :  " + queue.ToString());
            value = queue.Delete();
            Debug.Log("value :  " + value);

            Debug.Log("queue :  " + queue.ToString());

            queue.AddQueue(value);
            Debug.Log("queue.Lenght "+queue.Length);
            Debug.Log("queue :  " + queue.ToString());


            value = queue.Delete();
            value = queue.Delete();

            queue.AddQueue(value);
            Debug.Log("queue.Lenght "+queue.Length);
            Debug.Log("queue :  " + queue.ToString());


            value = queue.Delete();
            value = queue.Delete();

            queue.AddQueue(value);
            Debug.Log("queue.Lenght "+queue.Length);
            Debug.Log("queue :  " + queue.ToString());

        }
    }
}
