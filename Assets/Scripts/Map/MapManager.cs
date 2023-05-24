using System.Collections.Generic;
using tool;
using UnityEngine;

namespace Map
{
    public class MapManager : SingletonMono<MapManager>
    {
        private readonly MapGrid[,] _mapData = new MapGrid[MapWeight,MapHeight];
        [SerializeField] private MapGrid grid;
        private const int MapWeight = 200;
        private const int MapHeight = 100;
        private const float GridLenght = 1f;
        private List<MapGrid> _highlightGrids = new();
        private void Start()
        {
            InitMap();
        }
        
        private void InitMap()
        {
            for (int i = 0; i < MapWeight; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    var newGrid = Instantiate(grid,transform);
                    _mapData[i, j] = newGrid;
                    newGrid.transform.position = new Vector3(i * GridLenght, j * GridLenght);
                }
            }        
        }

        /// <summary>
        /// 高亮一个以某一格为中心的十字星区域
        /// </summary>
        public void HighLightCrossRange(int x,int y)
        {
            if (x < 0 || x > MapWeight || y < 0 || y > MapHeight)
            {
                Debug.LogError($"({x}{y})坐标超出范围");
                return;
            }
            HighLightOneGrid(x-1,y);
            HighLightOneGrid(x+1,y);
            HighLightOneGrid(x,y-1);
            HighLightOneGrid(x,y+1);
        }

        /// <summary>
        /// 高亮一个格子
        /// </summary>
        public void HighLightOneGrid(int x,int y)
        {
            if (x < 0 || x > MapWeight || y < 0 || y > MapHeight)
            {
                Debug.LogWarning($"({x}{y})坐标超出范围");
                return;
            }
            _mapData[x,y].HighLight();
            _highlightGrids.Add(_mapData[x,y]);
        }
    }
}