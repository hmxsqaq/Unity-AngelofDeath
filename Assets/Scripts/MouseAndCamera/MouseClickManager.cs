using System;
using Army;
using Map;
using tool;
using UnityEngine;

namespace MouseAndCamera
{
    public class MouseClickManager : SingletonMono<MouseClickManager>
    {
        [HideInInspector]public bool canClick = true; // 判断该帧是否处理点击事件
        private Vector3 MouseWorldPosition =>
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)); // 鼠标的世界坐标
        
        private MouseState _state = MouseState.AwaitOrder; // 鼠标的状态
        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0)&&ObjectAtMousePosition()&& canClick) 
            { 
                MouseClickAction(ObjectAtMousePosition().gameObject);
            }
            canClick = true;
        }

        private void MouseClickAction(GameObject clickObject)
        {
            switch (_state)
            {
                case MouseState.AwaitOrder:
                    AwaitOrderAction(clickObject);
                    break;
                case MouseState.SelectGrid:
                    
                    break;
                case MouseState.Loading:
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }   
        }
        
        /// <summary>
        /// 待命状态下,根据物体标签处理点击事件
        /// </summary>
        /// <param name="clickObject"></param>
        private void AwaitOrderAction(GameObject clickObject)
        {
            switch (clickObject.tag)
            {
                case "MyArmy":
                    var theArmy = clickObject.GetComponent<MyArmy>();
                    var theGrid = theArmy.LocalGrid;
                    MapManager.Instance.HighLightCrossRange(theGrid.X,theGrid.Y);
                    _state = MouseState.SelectGrid;
                    break;
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

        
        private void SelectGridAction(GameObject clickObject)
        {
            
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