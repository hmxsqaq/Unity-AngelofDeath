using UnityEngine;
using Map;
namespace Army
{
    public class MyArmy : MonoBehaviour,ICanBePutOnMap
    {
        private MapGrid _localGrid;
        public MapGrid LocalGrid
        {
            get => _localGrid;
            set
            {
                if (_localGrid is null)
                {
                    _localGrid = value;
                }
            }
        }
    }
}
