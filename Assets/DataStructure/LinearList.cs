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
        //TestLNodeList();
        
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
    
    // 测试队列
    private void TestLQueue()
    {
        LNodeQueue queue = new LNodeQueue();

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
// 链式存储堆栈
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

#endregion

#region 队列
class LQueue<T>
{

    public LQueue(int length)
    {
        _datas = new T[length];
        _front = -1;
        _after = -1;
    }

    // 数据
    private T[] _datas;
    // 前
    private int _front;
    // 后
    private int _after;

    // 长度
    public int Lenght
    {
        get
        {
            if(_front > _after)
                return _after + _datas.Length - _front;
            else
                return _after - _front;
        }
    }
    // 判断队列是否满了
    public bool IsFull()
    {
        return Lenght == _datas.Length - 1;
    }
    // 判断队列是否为空
    public bool IsEmptry()
    {
        return Lenght == 0;
    }

    // 加入队列
    public void AddQueue(T data)
    {
        if(IsFull())
        {
            Debug.LogError("队列满了");
            return;
        }
        _after = (_after + 1) % _datas.Length;
        _datas[_after] = data;
    }
    // 删除队列
    public T Delete()
    {
        if(IsEmptry())
        {
            Debug.LogError("队列为空");
            return default;
        }
        
        _front = (_front + 1) % _datas.Length;
        var temp = _datas[_front];
        _datas[_front] = default;

        return temp;
    }

    public override string ToString()
    {
        int f = _front;
        int a = _after;
        string str = "";
        while(f != a)
        {
            f = (f + 1) % _datas.Length;
            str += "" +_datas[f] + " , ";
        }

        return str;
    }
}

// 链式存储队列
class LNodeQueue
{

    public LNodeQueue()
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

#endregion

