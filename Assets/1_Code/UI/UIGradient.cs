using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 渐变UI代码
/// </summary>
[AddComponentMenu("UI/Effects/UIGradient")]
public class UIGradient : BaseMeshEffect
{
    public Direction direction = Direction.Horizontal;
    
    public Color32 oneColor = Color.white;
    public Color32 twoColor = Color.black;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive())
        {
            return;
        }
        //当前顶点数
        var count = vh.currentVertCount;
        if (count == 0)
            return;

        var vertexs = new List<UIVertex>();
        for (var i = 0; i < count; i++)
        {
            var vertex = new UIVertex();
            //获取顶点
            vh.PopulateUIVertex(ref vertex, i);
            vertexs.Add(vertex);
        }

        var topY = vertexs[0].position.y;
        var bottomY = vertexs[0].position.y;

        var liftX = vertexs[0].position.x;
        var rightX = vertexs[0].position.x;

        //找到边界点计算长宽
        for (var i = 1; i < count; i++)
        {
            var y = vertexs[i].position.y;
            var x = vertexs[i].position.x;

            if (y > topY)
                topY = y;
            else if (y < bottomY)
                bottomY = y;

            if (x < liftX)
                liftX = x;
            else if (x > rightX)
                rightX = x;
        }

        var height = topY - bottomY;
        var width = rightX - liftX;

        //插值运算颜色
        for (var i = 0; i < count; i++)
        {
            var vertex = vertexs[i];
            Color color = Color.white;
            switch (direction)
            {
                case Direction.Horizontal:
                    color = Color32.Lerp(oneColor, twoColor, (vertex.position.x - liftX) / width);
                    break;
                case Direction.Vertical:
                    color = Color32.Lerp(twoColor, oneColor, (vertex.position.y - bottomY) / height);
                    break;
            }
            // 不改变透明度
            color.a = vertex.color.a;
            
            vertex.color = color;
            
            vh.SetUIVertex(vertex, i);
        }
    }
}

public enum Direction
{
    Horizontal,
    Vertical,
}


