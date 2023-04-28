using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 保存文件相关代码
/// </summary>
public class RelatedToSaveFile
{
    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="strInfo">类容</param>
    /// <param name="path">绝对路径</param>
    public static void SaveData(string strInfo,string path)
    {
        //路径
        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);
        
        System.IO.FileInfo fi = new System.IO.FileInfo(path);
        System.IO.StreamWriter sw = fi.CreateText();
        sw.WriteLine(strInfo);
        sw.Close();
        sw.Dispose();
        AssetDatabase.Refresh();
    }
}
