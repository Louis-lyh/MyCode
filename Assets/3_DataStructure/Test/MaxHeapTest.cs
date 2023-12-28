using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LouisCode.DataStructure
{
    public class MaxHeapTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Test();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        // 
        private void Test()
        {
            var maxHeap = new MaxHeap(5);
            maxHeap.Insert(2);
            maxHeap.Insert(4);
            maxHeap.Insert(6);
            maxHeap.Insert(8);
            maxHeap.Insert(10);
            maxHeap.Insert(12);
            Debug.Log("MaxHeap: "+maxHeap.DeleteMax());
            Debug.Log("MaxHeap: "+maxHeap.DeleteMax());
            Debug.Log("MaxHeap: "+maxHeap.DeleteMax());
            Debug.Log("MaxHeap: "+maxHeap.DeleteMax());
            Debug.Log("MaxHeap: "+maxHeap.DeleteMax());
            Debug.Log("MaxHeap: "+maxHeap.DeleteMax());


        }
    }
}

