using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinBall : MonoBehaviour
{
    // 小球
    public GameObject BallSmall;
    // 碰撞体
    public CircleCollider2D CircleCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            RandomSpawn();
        }
    }

    /// <summary>
    /// 随机生成小球
    /// </summary>
    public void RandomSpawn()
    {
        // 初始化随机角度
        InitRandomAngle();
            
        for (int i = 0; i < 5; i++)
        {
            // 获取随机角度
            var angle = GetRandomAngle();
            Spawn(angle);
        }
    }
    
    /// <summary>
    /// 生成小球
    /// </summary>
    private void Spawn(float randomAngle)
    {
        var center = transform.position;
        var radius = 0.5f + 0.25f;

        var dir = Quaternion.Euler(0, 0, randomAngle) * Vector3.up;
        // 随机位置
        var randomPos = center + dir * radius;
        // 生成
        var item = GameObject.Instantiate(BallSmall, transform);
        item.GetComponent<CoinBallSamll>().Birth(Vector3.zero,randomPos);
    }

    #region 获得随机角度

    // 随机索引
    private List<int> _randomIndexList = new List<int>();
    
    // 平均角度
    private float _averageAngle = 0;
    
    // 开始角度
    private float _startAngle = 0;

    // 初始化随机角度
    private void InitRandomAngle()
    {
        // 平均角度
        _averageAngle = IsoscelesTrianglePinchAngle(0.5f + 0.25f, 0.25f * 2);
        // 最多个数
        var count = (int)(360 / _averageAngle); 
        // 记录索引
        _randomIndexList.Clear();
        for(int i = 0; i < count; i++)
            _randomIndexList.Add(i);
        // 随机开始角度
        _startAngle = Random.Range(0, 360);
        
        // log
        Debug.Log("count "+count);
    }
    
    /// <summary>
    /// 获取随机角度
    /// </summary>
    private float GetRandomAngle()
    {
       // 随机索引
       var index = _randomIndexList[Random.Range(0, _randomIndexList.Count)];
       _randomIndexList.Remove(index);
        
       // 角度
       var angle = _startAngle + _averageAngle * index;

       return angle;
    }

    /// <summary>
    /// 等腰三角形夹脚度数
    /// </summary>
    /// <param name="waistLength">腰长</param>
    /// <param name="baseLength">底边</param>
    /// <returns></returns>
    private float IsoscelesTrianglePinchAngle(float waistLength,float baseLength)
    {
        var cos = 1 - Mathf.Pow(baseLength, 2) / (2 * Mathf.Pow(waistLength, 2));
        var thetaRad  = Mathf.Acos(cos);
        var angle = thetaRad * (180.0f / Mathf.PI);
        
        return angle;
    }
    #endregion
}
