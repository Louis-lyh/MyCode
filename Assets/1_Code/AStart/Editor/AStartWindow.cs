using System.Collections.Generic;
using _1_Code.AStart;
using UnityEditor;
using UnityEngine;

public class AStartWindow : EditorWindow
{
    // 格子数量
    private int gridSize = 10;
    // 滑动窗口位置
    private Vector2 scrollPosition;
    // 算法下拉框
    private int dropdownIndex1 = 0;
    private string[] dropdownOptions1 = { "A*", "BFS", "Dijkstra" };
    // 格子类型    
    private int dropdownIndex2 = 0;
    private string[] dropdownOptions2 = { "路", "起点", "终点","障碍","沙地","草地"};

    // 滑动条进度
    private float sliderValue = 0.5f;
    
    // 地图数据
    private MapData _mapData;
    
    // 按钮贴图
    private Dictionary<Color, Texture2D> colorTextures = new Dictionary<Color, Texture2D>();
    
    // 路径
    List<int> path = new List<int>();
    Dictionary<int,int> cameFrom = new Dictionary<int,int>();
    
    [MenuItem("MyCode/A* Window")]
    public static void ShowWindow()
    {
        GetWindow<AStartWindow>("A*");
    }

    void OnEnable()
    {
        // 初始化地图
        _mapData = new MapData(gridSize);
    }

    void OnGUI()
    {
        // 左上角下拉框
        EditorGUILayout.BeginVertical();
        {
            // 选择算法 下拉框
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField($"选择算法");
            dropdownIndex1 = EditorGUILayout.Popup(dropdownIndex1, dropdownOptions1, GUILayout.Width(300));
            
            // 格子内线下拉框
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField($"格子类型");
            dropdownIndex2 = EditorGUILayout.Popup(dropdownIndex2, dropdownOptions2,GUILayout.Width(300));
        }
        EditorGUILayout.EndVertical();

        // 滚动视图
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        {
            // 网格布局
            DrawGrid();
            
            // 输入框
            EditorGUILayout.Space(20);
            DrawCentered(() => {
                gridSize = EditorGUILayout.IntField("Grid Size", gridSize);
                gridSize = Mathf.Clamp(gridSize, 1, 50);  // 限制网格大小
                // 修改大小出现创建地图
                if(_mapData.Size != gridSize)
                    _mapData = new MapData(gridSize);
            });
            
            // 滑动条
            EditorGUILayout.Space(20);
            DrawCentered(() => {
                sliderValue = EditorGUILayout.Slider("Slider", sliderValue, 0f, 1f);
            });
            
            // 技术按钮
            EditorGUILayout.Space(20);
            DrawCentered(() => {
                if (GUILayout.Button("计算"))
                {
                    if(dropdownIndex1 == 0)
                        path = PathFinding.AStart(_mapData.MapDataArray,_mapData.StartIndex,_mapData.EndIndex,out cameFrom);
                    else if(dropdownIndex1 == 1)
                        path = PathFinding.BreadthFirstSearch(_mapData.MapDataArray,_mapData.StartIndex,_mapData.EndIndex,out cameFrom);
                    else if(dropdownIndex1 == 2)
                        path = PathFinding.Dijkstra(_mapData.MapDataArray,_mapData.StartIndex,_mapData.EndIndex,out cameFrom);
                }
            });
            
        }
        EditorGUILayout.EndScrollView();
    }

    private void DrawGrid()
    {
        EditorGUILayout.Space(10);
        
        DrawCentered(() => {
            GUILayout.Label($"Button Grid ({gridSize}x{gridSize})", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);
            
            // 计算按钮尺寸
            float btnSize = Mathf.Min(50, (position.width - 40) / gridSize);
            
            // 绘制网格
            for (int y = 0; y < gridSize; y++)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    // 水平居中
                    GUILayout.FlexibleSpace();
                    
                    for (int x = 0; x < gridSize; x++)
                    {
                        var gridType = _mapData.GetMapItemType(x, y);
                        var index = x + gridSize * y;
                        
                        // 按钮颜色
                        Color btnColor = GetButtonColor(gridType,path.Contains(index));
                        
                        // 创建按钮样式
                        GUIStyle btnStyle = new GUIStyle(GUI.skin.button);
                        // 重置状态确保完全清除默认样式
                        btnStyle.normal = new GUIStyleState();
                        btnStyle.active = new GUIStyleState();
                        btnStyle.normal.background = GetColorTexture(btnColor * 0.7f);
                        btnStyle.active.background = GetColorTexture(btnColor); // 点击时变亮
                        btnStyle.normal.textColor = Color.white;
                        // 按钮名称
                        var buttonName = GetButtonName(gridType,index);
                        
                        if (GUILayout.Button(buttonName, btnStyle,GUILayout.Width(btnSize), GUILayout.Height(btnSize)))
                        {
                            // log
                            Debug.Log($"Clicked button: ({x}, {y}) => {(MapItemType)dropdownIndex2}");
                            // 修改地图数据
                            _mapData.ChangeMapData(x, y,(MapItemType)dropdownIndex2);
                        }
                    }
                    
                    GUILayout.FlexibleSpace();
                }
                EditorGUILayout.EndHorizontal();
            }
        });
    }

    // 辅助方法：居中绘制内容
    private void DrawCentered(System.Action content)
    {
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            
            EditorGUILayout.BeginVertical();
            {
                content?.Invoke();
            }
            EditorGUILayout.EndVertical();
            
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndHorizontal();
    }

    // 窗口改变大小时重绘
    void OnInspectorUpdate()
    {
        Repaint();
    }

    private Color GetButtonColor(MapItemType gridType,bool isPath = false)
    {
        ColorUtility.TryParseHtmlString("#008080ff", out Color colorIsPath);
        
        switch (gridType)
        {
            case MapItemType.Road:
                if(isPath)
                    return colorIsPath;
                return Color.white;
            case MapItemType.OBS:
                return Color.black;
            case MapItemType.Start:
                return Color.cyan;
            case MapItemType.End:
                return Color.blue;
            case MapItemType.Sand:
                if(isPath)
                    return colorIsPath;
                return Color.yellow;
            case MapItemType.Grass:
                if(isPath)
                    return colorIsPath;
                return Color.green;
        }
        
        return Color.white;
    }

    private string GetButtonName(MapItemType gridType,int index)
    {
        // 广度优先搜索
        if (gridType == MapItemType.Road)
        {
            if (cameFrom.ContainsKey(index))
            {
                var value = cameFrom[index] - index;
                if (value == 1) return "→";
                if (value > 1) return "↓";
                if (value == -1) return "←";
                if (value < -1) return "↑";
            }
        }

        return gridType.ToString();
    }

    // 创建纯色纹理
    private Texture2D GetColorTexture(Color color)
    {
        if (!colorTextures.ContainsKey(color))
        {
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color);
            tex.Apply();
            colorTextures[color] = tex;
        }
        return colorTextures[color];
    }
}