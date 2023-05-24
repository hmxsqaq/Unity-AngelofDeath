using tool;
using UnityEngine;

namespace MouseAndCamera
{
    public class MouseClickManager : SingletonMono<MouseClickManager>
    {
        [HideInInspector]public bool canClick = true; // 判断该帧是否处理点击事件
        private Vector3 MouseWorldPosition =>
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0)&&ObjectAtMousePosition()&& canClick) 
            { 
                ClickAction(ObjectAtMousePosition().gameObject);
            }
            canClick = true;
        }
        
        /// <summary>
        /// 根据物体标签处理点击事件
        /// </summary>
        /// <param name="clickObject"></param>
        private void ClickAction(GameObject clickObject)
        {
            switch (clickObject.tag)
            {
                case "MapGrid":
                    CameraMove.Instance.canMove = true;
                    CameraMove.Instance.mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    break;
                default:
                    CameraMove.Instance.canMove = true;
                    CameraMove.Instance.mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Debug.Log("没有指定标签");
                    break;
            }
        }
        
        /// <summary>
        /// 获取鼠标世界位置的碰撞体
        /// </summary>
        /// <returns></returns>
        private Collider2D ObjectAtMousePosition()
        {
            return Physics2D.OverlapPoint(MouseWorldPosition);
        }
    }
}