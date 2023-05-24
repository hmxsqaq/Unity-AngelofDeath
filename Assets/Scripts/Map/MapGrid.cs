using UnityEngine;

namespace Map
{
    public class MapGrid : MonoBehaviour
    {
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
