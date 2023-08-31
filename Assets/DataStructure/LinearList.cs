using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class LinearList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 测试链表
        TestLNodeList();
        
        // 测试栈
        TestLStack();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // 测试链表
    private void TestLNodeList()
    {
        // 链表
        LNodeList lNodeList = new LNodeList();
        
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
        LNodeListStack lNodeListStack = new LNodeListStack();
        
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

}

#region 链表
class LNode
{
    public int Data;
    public LNode Next;
}

class LNodeList
{
    // 头节点
    public LNode Header;
    
    // 获取长度
    public int Length()
    {
        LNode temp = Header;
        int length = 0;
        while(temp != null)
        {
            temp = temp.Next;
            length++;
        }
        
        return length;
    }

    // 通过序号获取节点
    public LNode FindeIndex(int index)
    {
        LNode temp = Header;
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

    // 通过值获取节点
    public LNode FindeValue(int data)
    {
        LNode temp = Header;
        while(temp != null && temp.Data != data)
            temp = temp.Next;
        
        return temp;
    }
    // 插入
    public LNode Insert(LNode node,int index)
    {
        LNode temp = new LNode();
        temp.Data = node.Data;
        // 插入到第一个
        if(index == 0)
        {   
            temp.Next = Header;
            Header = temp;
            return Header;
        }
        // 找到前一个节点
        LNode front = FindeIndex(index - 1);

        // 数据错误
        if(front == null)
        {
            Debug.LogError($"参数错{index}");
            return front;
        }
        else
        {
            temp.Next = front.Next;
            front.Next = temp;
            return Header;
        }
    }
    // 删除
    public LNode Delete(int index)
    {
        // 删除头节点
        if(index == 0)
        {
            Header = Header.Next;
            return Header;
        }
        else
        {
            LNode temp = FindeIndex(index - 1);
            if(temp == null)
            {
                Debug.LogError($"参数错{index}");
                return Header;
            }

            if(temp.Next == null)
            {
                Debug.LogError($"参数错{index}");
                return Header;
            }
            LNode oldNode = temp.Next;
            temp.Next = oldNode.Next;

            return Header;
        }
    }

    public override string ToString()
    {
        string str = "";
        for (int i = 0; i < Length(); i++)
        {
            var node = FindeIndex(i);
            str += $"{i}:{node.Data}\n";
        }

        return str;
    }
}
#endregion

#region 堆

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
    public T[] Datas;
    // 长度
    public int Length => Datas.Length;
    
    // 栈顶位置
    public int Top;
    // 入栈
    public void Push(T data)
    {
        if (Top == Length - 1)
        {
            Debug.LogError("栈满了");
            return;
        }
        Datas[++Top] = data;
    }
    // 出栈
    public T Pop()
    {
        if (Top == -1)
        {
            Debug.LogError("栈空了");
            return default(T);
        }
        
        return  Datas[Top--];
    }
}

class LNodeListStack
{

    public LNodeListStack()
    {
        Datas = new LNodeList();
        Datas.Header = new LNode();
    }

    // 栈
    public LNodeList Datas;
    
    // 长度
    public int Length => Datas.Length();
    
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

#endregion

