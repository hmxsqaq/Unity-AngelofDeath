using Framework;
using UnityEngine;

namespace Map
{
    public class MapManager : SingletonMono<MapManager>
    {
        private MapGrid[] _mapData;
    }
}