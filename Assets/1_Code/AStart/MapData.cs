namespace _1_Code.AStart
{
    /// <summary>
    /// 地图元素
    /// </summary>
    public enum MapItemType
    {
        Road,   // 路
        Start, // 起点
        End,   // 终点
        OBS,   // 障碍
    }

    public class MapData
    {
        // 数据
        private MapItemType[][] _mapData;
        public MapItemType[][] MapDataArray=> _mapData;
        // 起点
        private int _startIndex;
        public int StartIndex => _startIndex;
        // 终点
        private int _endIndex;
        public int EndIndex => _endIndex;
        
        // 尺寸
        public int Size=> _mapData.Length;
        
        public MapData(int mapSize)
        {
            _mapData = new MapItemType[mapSize][];
            for (int i = 0; i < mapSize; i++)
                _mapData[i] = new MapItemType[mapSize];

            for (int i = 0; i < _mapData.Length; i++)
            {
                for (int j = 0; j < _mapData[i].Length; j++)
                {
                    _mapData[i][j] = MapItemType.Road;
                }
            }

            _startIndex = 0;
            _mapData[0][0] = MapItemType.Start;
            
            _endIndex = mapSize * mapSize - 1;
            _mapData[mapSize - 1][mapSize - 1] = MapItemType.End;
        }
        
        /// <summary>
        /// 修改地图数据
        /// </summary>
        public void ChangeMapData(int x,int y, MapItemType mapType)
        {
            var index = x + y * _mapData.Length;

            switch (mapType)
            {
                case MapItemType.Start:
                    if (_startIndex >= 0)
                    {
                        var startX = _startIndex % _mapData.Length;
                        var startY = _startIndex / _mapData[0].Length;
                        _mapData[startX][startY] = MapItemType.Road;
                    }

                    _startIndex = index;
                    break;
                
                case MapItemType.End:
                    if (_endIndex >= 0)
                    {
                        var endX = _endIndex % _mapData.Length;
                        var endY = _endIndex / _mapData[0].Length;
                        _mapData[endX][endY] = MapItemType.Road;
                    }

                    _endIndex = index;
                    break;
                
            }

            // 记录
            _mapData[x][y] = mapType;
        }

        /// <summary>
        /// 获取地图数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public MapItemType GetMapItemType(int x, int y)
        {
            return _mapData[x][y];
        }
    }
}