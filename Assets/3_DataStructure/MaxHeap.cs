using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LouisCode.DataStructure
{
    public class MaxHeap
    {
        public MaxHeap(int max)
        {
            _items = new double[max];
            _itemCount = 0;
        }
        // 堆
        private double[] _items;
        // 元素数量
        private int _itemCount;
        // 数量
        public int Count => _itemCount;
        
        // 是否满了
        public bool IsFull()
        {
            return _itemCount == _items.Length;
        }
        // 是否为空
        public bool IsEmpty()
        {
            return _itemCount == 0;
        }

        // 插入
        public void Insert(double item)
        {
            if (IsFull())
            {
                Debug.LogError("最大堆满了无法插入");
                return;
            }
            
            // 数量加一
            _itemCount++;
            int i = _itemCount - 1;
            for (; i >0 && _items[(i - 1) / 2] < item; i = (i - 1) / 2)
            {
                _items[i] = _items[(i - 1) / 2];
            }

            _items[i] = item;
        }
        
        // 返回最大元素
        public double DeleteMax()
        {
            if (IsEmpty())
            {
                Debug.LogError("最大堆为空退出");
                return -1;
            }

            var maxItem = _items[0];
            // 没有了退出
            if (_itemCount == 0)
                return maxItem;
            
            // 把堆最后一个放入第一个如何从上往下过滤
            var temp = _items[_itemCount - 1];
            int parent = 0;
            for (; parent * 2 + 1 < _itemCount;)
            {
                // 找到最大儿子
                var maxChild = parent * 2 + 1;
                if (maxChild + 1 < _itemCount && _items[maxChild + 1] > _items[maxChild])
                    maxChild++;
                // 小于儿子交换
                if (temp < _items[maxChild])
                    _items[parent] = _items[maxChild];
                // 大于儿子突出循环
                else
                    break;
                parent = maxChild;
            }
            // 放入找到的合适位置
            _items[parent] = temp;
            // 数量减一
            _itemCount--;
            
            return maxItem;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < _itemCount; i++)
            {
                str += "" + _items[i];
                if (i < _itemCount - 1)
                    str += " ";
            }
            return str;
        }
    }  
}


