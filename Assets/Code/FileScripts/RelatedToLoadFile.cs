using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 与读取文件相关代码
/// </summary>
public class RelatedToLoadFile
{
    #region 读取文件中制定文件
    /// <summary>
    /// 读取文件中的文本文件
    /// </summary>
    /// <param name="folderPath">文件夹绝对路径</param>
    /// <param name="searchPattern">搜索字段</param>
    /// <returns></returns>
    public static List<TextAsset> GetFileJson(string folderPath, string searchPattern)
    {
        List<TextAsset> fileList = new List<TextAsset>();
        var dir = new DirectoryInfo(folderPath);
        //获取文件
        var files = dir.GetFiles(searchPattern, SearchOption.AllDirectories);
        foreach (var file in files)
        {
            if(!file.Name.EndsWith(searchPattern))
                continue;
            
            var mapJson = AssetDatabase.LoadAssetAtPath<TextAsset>(DataPathToAssetPath(file.FullName));
            fileList.Add(mapJson);
        }
        return fileList;
    }
    //去掉Assets前的地址
    private static string DataPathToAssetPath(string path)
    {
        return Application.platform == RuntimePlatform.WindowsEditor
            ? path.Substring(path.IndexOf("Assets\\"))
            : path.Substring(path.IndexOf("Assets/"));
    }
    #endregion
}
