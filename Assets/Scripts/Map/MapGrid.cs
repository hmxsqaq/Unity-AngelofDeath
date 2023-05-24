using UnityEngine;

namespace Map
{
    public class MapGrid : MonoBehaviour
    {
        private int _x=-1, _y=-1;

        public int X
        {
            get => _x;
            set
            {
                if (_x == -1)
                {
                    _x = value;
                }
            }
        }
        public int Y
        {
            get => _y;
            set
            {
                if (_y == -1)
                {
                    _y = value;
                }
            }
        }

        private ICanBePutOnMap _bePutItem;
        private SpriteRenderer TheSpriteRenderer => GetComponent<SpriteRenderer>();
        
        /// <summary>
        /// 高亮格子
        /// </summary>
        public void HighLight()
        {
            TheSpriteRenderer.color = new Color(0, 0, 0, 0.7f);
        }
    }
}
