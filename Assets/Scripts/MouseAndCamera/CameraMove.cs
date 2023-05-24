using tool;
using UnityEngine;

namespace MouseAndCamera
{
    public class CameraMove : SingletonMono<CameraMove>
    {
        public Transform upLeftPoint, downRightPoint; // 限制摄像机位置的两个点
        [HideInInspector]public Vector3 mouseStartPosition; // 鼠标的启示位置
        [HideInInspector] public bool canMove; // 判断摄像机是否可以移动
        //private float _sizeDistance;
        [SerializeField] private float moveZoom; // 摄像机移动快慢的缩放值
        //private bool _isSlide;
        private void Update()
        {
            MouseCheck();
        }

        private void FixedUpdate()
        {
            // if (!_isSlide)
            // {
            //     Move();
            // }
            // else
            // {
            //     Slide();
            // }
            
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

        // public void SlideTo(Vector3 target)
        // {
        //     _target = target;
        //     _isSlide = true;
        // }

        // private void Slide()
        // {
        //     transform.position = Vector2.Lerp(transform.position, _target, 0.07f);
        //     transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        //     if (Vector2.Distance(transform.position,_target)<1f)
        //     {
        //         _isSlide = false;
        //     }
        // }
        
        //悲
        // private void ChangeSize()
        // {
        //     _sizeDistance = Input.GetAxis("Mouse ScrollWheel");
        //     var predictedSize = Camera.main.orthographicSize + _sizeDistance*sizeChangeSpeedZoom;
        //     if (predictedSize<5||predictedSize>=5+(downRightPoint.position.x-upLeftPoint.position.x)/9f)
        //     {
        //         return;
        //     }
        //     Camera.main.orthographicSize += _sizeDistance*sizeChangeSpeedZoom;
        // }
        
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