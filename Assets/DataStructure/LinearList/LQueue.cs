using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LouisCode.DataStructure
{
    class Queue<T>
    {
    
        public Queue(int length)
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

}

