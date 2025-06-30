using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLogic.Hotfix
{
    public class ReboundBall : MonoBehaviour
    {
        /// <summary>
        /// 碰撞体
        /// </summary>
        public CircleCollider2D CircleCollider2D;

        private void Awake()
        {
            CircleCollider2D = transform.GetComponent<CircleCollider2D>();
        }

        private void Update()
        {
            // 移动
            Move(Time.deltaTime);
        }


        #region 射线移动
        /// <summary>
        /// 速度
        /// </summary>
        public float Speed = 1;
        
        /// <summary>
        /// 下一个位置
        /// </summary>
        private Vector3 _nextPos = Vector3.one * int.MaxValue;

        /// <summary>
        /// 移动方向
        /// </summary>
        private Vector2 _moveDir = Vector3.up;
        
        /// <summary>
        /// 移动距离
        /// </summary>
        private float _moveDis = 0;

        /// <summary>
        /// 前一次的碰撞体
        /// </summary>
        private int _preCollider = -1;
        
        public void Move(float time)
        {
            // 修改位置
            if(_nextPos != Vector3.one * int.MaxValue)
                transform.localPosition = _nextPos;
            
            Vector2 curPos = transform.position;
            
            // 移动距离
            _moveDis = time * Speed;
            // 下一帧位置
            _nextPos = curPos + _moveDir * _moveDis;

            // layer
            var layerMask = 1 << LayerMask.NameToLayer("Default");
            // 碰撞
            var hit = Physics2D.CircleCast(curPos,CircleCollider2D.radius,_moveDir, 0,layerMask);
            // 存在反弹
            if (hit.collider != null)
            {
               
                // log
                string log = "";
                
                // 当前角度
                var curAngle = Vector2.Angle(-hit.normal, _moveDir);
                
                if(_preCollider ==  hit.collider.GetHashCode())
                    return;

                // log
                log += $"_moveDir {_moveDir} , hit.normal {hit.normal} , urAngle {curAngle} ， _preCollider {_preCollider} , hit.GetHashCode() {hit.collider.GetHashCode()} , ";
                
                _preCollider = hit.collider.GetHashCode();
                
                // 碰撞到碰撞体
                OnHitCollider2D(hit.collider);

                // 角度太小随机方向
                if (curAngle < 5)
                {
                    // 判断向量B是否在向量A的左侧或右侧
                    Vector2 aDir = -hit.normal; // 参考方向（例如向右）
                    Vector2 bDir = _moveDir; // 待判断的向量
                    float cross = aDir.x * bDir.y - aDir.y * bDir.x;
                    var crossDir = 1;
                    // B在A的左侧
                    if (cross > 0)
                        crossDir = -1;
                    // B在A的右侧
                    else if (cross < 0)
                        crossDir = 1;
                    // B与A同方向或反向
                    else
                        crossDir = 1;

                    // 添加微小随机方向偏移
                    float angle = Random.Range(10, 20);
                    if(crossDir < 1)
                        angle = Random.Range(-20, -10);
                
                    _moveDir = Quaternion.Euler(0, 0, angle) * _moveDir;
                    
                    // log
                    log += $"newDir {_moveDir} , cross {cross} ,crossDir {crossDir} , angle {angle} , ";
                }
                
                _moveDir = Vector3.Reflect(_moveDir, hit.normal);
                _moveDir = _moveDir.normalized;
    
                // 下一帧位置
                _nextPos = curPos + _moveDir * _moveDis;

                log += $"Reflect {_moveDir}, _moveDis {_moveDis}, curPos {curPos} _nextPos {_nextPos},";
                
                // log
                Debug.Log(log);
            }
        }
        #endregion
        
        private void OnHitCollider2D(Collider2D other)
        {
            if (other.gameObject.name.Contains("CoinBall"))
            {
                other.GetComponent<CoinBall>().RandomSpawn();
            }
        }
    }
}