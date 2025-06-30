using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI 适配工具
/// </summary>
public class UIFixCanvasTool : MonoBehaviour
{
    private void Awake()
    {
        FixResolution();
    }
	/// <summary>
	/// 根据屏幕比例设置屏幕适配类型
	/// </summary>
    public void FixResolution()
    { 
        CanvasScaler cs = GetComponent<CanvasScaler>();
	
        float sWToH = cs.referenceResolution.x * 1.0f / cs.referenceResolution.y;
        float vWToH = Screen.width * 1.0f / Screen.height;
        if(sWToH > vWToH)
            cs.matchWidthOrHeight = 0;//匹配宽
        else
            cs.matchWidthOrHeight = 1;//匹配高
    }
	/// <summary>
	/// 获得匹配后的坐标
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
    public Vector3 GetNewPos(Vector3 value)
    {
        var cs = transform.GetComponent<CanvasScaler>();
        if(cs.matchWidthOrHeight == 0)
            //匹配宽度时仅按照宽度计算
            return value*cs.referenceResolution.x/Screen.width;
        else
            //匹配高度时仅按照高度计算
            return value*cs.referenceResolution.y/Screen.height;
    }
    /// <summary>
    /// 获得匹配后的值
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float GetNewPos(float value)
    {
        var cs=transform.GetComponent<CanvasScaler>();
        if(cs.matchWidthOrHeight == 0)
            //匹配宽度时仅按照宽度计算
            return value*cs.referenceResolution.x/Screen.width;
        else
            //匹配高度时仅按照高度计算
            return value*cs.referenceResolution.y/Screen.height;
    }
    /// <summary>
    /// 获得匹配前的坐标
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Vector3 GetOldPos(Vector3 value)
    {
        var cs = transform.GetComponent<CanvasScaler>();
        if(cs.matchWidthOrHeight == 0)
            //匹配宽度时仅按照宽度计算
            return value / (cs.referenceResolution.x / Screen.width);
        else
            //匹配高度时仅按照高度计算
            return value / (cs.referenceResolution.y / Screen.height);
    }
    
}
