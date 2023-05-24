using tool;
using UnityEngine;

namespace MouseAndCamera
{
    public class CameraMove : SingletonMono<CameraMove>
    {
        public Transform upLeftPoint, downRightPoint; // 限制摄像机位置的两个点
        [HideInInspector]public Vector3 mouseStartPosition; // 鼠标的启示位置
        [HideInInspector] public bool canMove; // 判断摄像机是否可以移动
        [SerializeField] private float moveZoom; // 摄像机移动快慢的缩放值
        private void Update()
        {
            MouseCheck();
        }

        private void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// 摄像机移动逻辑
        /// </summary>
        private void Move()
        {
            if (!canMove) return;
            var mouseNowPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var predictedPos = transform.position-(mouseNowPosition - mouseStartPosition);
            if (!(predictedPos.x < upLeftPoint.position.x || predictedPos.x > downRightPoint.position.x))
            {
                transform.position = new Vector3(predictedPos.x*moveZoom, transform.position.y, -10);
            }
            if (!(predictedPos.y < downRightPoint.position.y || predictedPos.y > upLeftPoint.position.y))
            {
                transform.position = new Vector3(transform.position.x*moveZoom, predictedPos.y, -10);
            }
        }

        /// <summary>
        /// 检测何时停止摄像机移动
        /// </summary>
        private void MouseCheck()
        {
            if (Input.GetMouseButtonUp(0))
            {
                canMove = false;
            }
        }
    }
}