using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
#endregion