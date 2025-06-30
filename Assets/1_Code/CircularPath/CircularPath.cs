using System;
using UnityEngine;

/// <summary>
/// 圆形路径
/// </summary>
public class CircularPath : MonoBehaviour
{
    // 单例
    public static CircularPath Instance;
    // 半径
    public float Radius = 5;
    private float _radius = 1;
    // 中心点
    public Vector3 Center = Vector3.zero;
    private Vector3 _center = Vector3.zero;
    // 个数
    public int PointCount = 360;
    private int _pointCount = 0;
    // 线
    public LineRenderer LineRenderer;
    // 周长 
    public float perimeter;
    
    public void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(Vector3 center,float radius)
    {
        Center = center;
        Radius = radius;
    }

    /// <summary>
    /// 获得法线
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Vector3 GetNormalBuyAngle(float angle)
    {
        var dir =  Quaternion.Euler(0, 0, angle) * Vector2.up;
        return dir.normalized;
    }

    /// <summary>
    /// 获取位置
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Vector3 GetPos(float angle)
    {
        var dir =  Quaternion.Euler(0, 0, angle) * Vector2.up;
        var pos = Center + dir * Radius;
        return pos;
    }

    /// <summary>
    /// 通过长度获得位置
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public Vector3 GetPosBuyLength(float length)
    {
        // 长度转为角度
        var angle = LengthToAngle(length);
        
        return GetPos(angle);
    }

    /// <summary>
    /// 根据x轴获得位置
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPosBuyX(float x)
    {
        // 获取y轴坐标
        var yArray = GetYCoordinates(Center,Radius, x);
        // 在圆上
        if (yArray.Length > 0)
        {
            // log
            Debug.Log($"{x} =>  yArray = {yArray[0]} {yArray[1]}");
            
            return new Vector3(x, yArray[0], 0);
        }
        // 不在圆上
        else
        {
            // log
            Debug.Log($"x = {x} 不在 圆上");
            
            // 超过右边返回最右边的点
            if(x > _center.x)
                return new Vector3(_center.x + _radius,_center.y,0);
            // 在左边返回最左边的点
            else 
                return new Vector3(_center.x - _radius,_center.y);
        }
    }

    /// <summary>
    /// 获得角度
    /// </summary>
    /// <returns></returns>
    public float GetAngle(Vector3 pos)
    {
        var dir = pos - Center;
        var angele = Vector3.Angle(Vector3.up, dir);
        var z = Vector3.Cross(Vector3.up, dir);

        if (z.z > 0)
            return angele;
        else
            return -angele;
    }

    /// <summary>
    /// 角度转为长度
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public float AngleToLength(float angle)
    {
        // 角度
        angle = angle % 360;
        var length = perimeter * (angle / 360f);

        return length;
    }

    /// <summary>
    /// 长度转为角度
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public float LengthToAngle(float length)
    {
        var angle = length / perimeter * 360;
        return angle;
    }
    
    // 定义容差值，用于处理浮点数精度问题
    private const double Tolerance = 1e-10;
    
    /// <summary>
    /// 获取y轴坐标
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    private static float[] GetYCoordinates(Vector3 center, float radius, float x)
    {
        float deltaX = x - center.x;
        float deltaXSquared = deltaX * deltaX;
        float radiusSquared = radius * radius;
        
        // 检查x坐标是否在圆外（超出水平范围）
        if (deltaXSquared > radiusSquared + Tolerance)
        {
            return new float[0]; // 返回空数组表示无解
        }
        
        // 处理浮点精度边界情况
        float discriminant = radiusSquared - deltaXSquared;
        if (discriminant < 0)
        {
            // 在容差范围内视为相切
            discriminant = 0;
        }
        
        // 计算y坐标
        float sqrtDiscriminant = (float)Math.Sqrt(discriminant);
        float y1 = center.y + sqrtDiscriminant;
        float y2 = center.y - sqrtDiscriminant;
        
        // x轴与球相切时返回两个相同的y值
        return new[] { y1, y2 };
    }

    public void Update()
    {
        // 检查配置是否变化
        if(!CheckConfig())
            return;
        
        // 绘制路径
        DrawPath();
    }

    // 检查配置变化
    private bool CheckConfig()
    {
        if(_radius == Radius && Center == _center && PointCount == _pointCount)
            return false;
        
        // 配置
        _radius = Radius;
        _center = Center;
        _pointCount = PointCount;
        
        // 周长
        perimeter = 2 * Mathf.PI * _radius;

        return transform;
    }

    /// <summary>
    /// 绘制路径
    /// </summary>
    private void DrawPath()
    {
        // 计算路径点
        var avgAngle = 360f / _pointCount;
        Vector3[] pathArray = new Vector3[_pointCount];
        
        for (int i = 0; i < _pointCount; i++)
        {
            var dir =  Quaternion.Euler(0, 0, avgAngle * i) * Vector2.down;
            var pos = Center + dir * Radius;
            pos.z = 0;
            pathArray[i] = pos;
        }
        LineRenderer.positionCount = _pointCount;
        LineRenderer.SetPositions(pathArray);
    }
}
