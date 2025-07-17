using System;
using System.Collections.Generic;

namespace _1_Code.AStart
{
    /// <summary>
    /// 寻路算法
    /// </summary>
    public static class PathFinding
    {
        /// <summary>
        /// 广度优先搜索
        /// </summary>
        /// <param name="mapData"></param>
        public static  List<int> BreadthFirstSearch(MapItemType[][] mapData,int start,int end,out Dictionary<int,int> cameFrom)
        {
            // 搜索方向
            int[][] dir = new int[4][] {new []{0,1},new []{-1,0},new []{1,0},new []{0,-1} };
            
            // 需要检查的点
            Queue<int> frontier = new Queue<int>();
            frontier.Enqueue(start);
            
            // 来自那个格子
            cameFrom = new Dictionary<int,int>();
            cameFrom[start] = -1;
            
            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();
                var curX = current % mapData.Length;
                var curY = current / mapData.Length;
                
                // 找到目标点退出
                if(current == end)
                    break;
                
                // 遍历四周的点
                for (int i = 0; i < dir.Length; i++)
                {
                    var nextX = curX + dir[i][0];
                    var nextY = curY + dir[i][1];
                    var next = nextX + mapData.Length * nextY;
                    
                    // 位置必须合法
                    if (nextX < mapData.Length && nextY < mapData.Length && nextX >= 0 && nextY >= 0)
                    {
                        if (!cameFrom.ContainsKey(next) && mapData[nextX][nextY] != MapItemType.OBS)
                        {
                            frontier.Enqueue(next);
                            cameFrom[next] = current;
                        }
                    }
                }
            }

            // 获取路径
            var pathIndex = end;
            List<int> path = new List<int>();

            while (pathIndex != start)
            {
                path.Add(pathIndex);
                pathIndex = cameFrom[pathIndex];
            }
            path.Add(start);
            // 翻转
            path.Reverse();

            return path;
        }
        
        /// <summary>
        /// 统一成本搜索
        /// </summary>
        /// <param name="mapData"></param>
        public static  List<int> Dijkstra(MapItemType[][] mapData,int start,int end,out Dictionary<int,int> cameFrom)
        {
            // 搜索方向
            int[][] dir = new int[4][] {new []{0,1},new []{-1,0},new []{1,0},new []{0,-1} };
            
            // 需要检查的点
            Queue<int> frontier = new Queue<int>();
            frontier.Enqueue(start);
            
            // 来自那个格子
            cameFrom = new Dictionary<int,int>();
            cameFrom[start] = -1;
            
            // 记录移动成本
            var costSoFar = new Dictionary<int,int>();
            costSoFar[start] = 0;
            
            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();
                var curX = current % mapData.Length;
                var curY = current / mapData.Length;
                
                // 找到目标点退出
                if(current == end)
                    break;
                
                // 遍历四周的点
                for (int i = 0; i < dir.Length; i++)
                {
                    var nextX = curX + dir[i][0];
                    var nextY = curY + dir[i][1];
                    var next = nextX + mapData.Length * nextY;
                    
                    // 位置必须合法
                    if (nextX < mapData.Length 
                        && nextY < mapData.Length 
                        && nextX >= 0 && nextY >= 0 
                        && mapData[nextX][nextY] != MapItemType.OBS)
                    {
                        // 类型
                        var itemType = mapData[nextX][nextY];
                        // 移动成本
                        var newCost = costSoFar[current] + GetCostSoFar(itemType);
                        
                        if (!cameFrom.ContainsKey(next) || newCost < costSoFar[next])
                        {
                            // 更新移动成本
                            costSoFar[next] = newCost;
                            frontier.Enqueue(next);
                            cameFrom[next] = current;
                        }
                    }
                }
            }

            // 获取路径
            var pathIndex = end;
            List<int> path = new List<int>();

            while (pathIndex != start)
            {
                path.Add(pathIndex);
                pathIndex = cameFrom[pathIndex];
            }
            path.Add(start);
            // 翻转
            path.Reverse();

            return path;
        }

        /// <summary>
        /// 获得移动成本
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static int GetCostSoFar(MapItemType type)
        {
            switch (type)
            {
                case MapItemType.Road:
                    return 1;
                case MapItemType.OBS:
                    return 10000;
                case MapItemType.Start:
                    return 1;
                case MapItemType.End:
                    return 1;
                case MapItemType.Sand:
                    return 5;
                case MapItemType.Grass:
                    return 3;
            }

            return 1;
        }

    }
}