using UnityEngine;

public class CircularPathUnit : MonoBehaviour
{
    // 当前角度
    public float CurAngle;
    // 速度
    public float Speed = 1;
    // 移动状态
    public string MoveState;
    // 当前法线
    public Vector3 Normal;
    
    // 击飞距离
    public float FlyDis = -2;
    // 击飞高度
    public float FlyHeight = 2;
    // 飞行速度
    public float FlySpeed = 1;
    // 飞行进度
    public float FlyProgress;
    // 开始位置
    public Vector3 StartPos;
    // 结束位置
    public Vector3 EndPos;
    // 中间位置
    public Vector3 CenterPos;

    // Start is called before the first frame update
    void Start()
    {
        // 移动
        MoveState = "move";
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(Vector3 startPos)
    {
        var pos = CircularPath.Instance.GetPosBuyX(startPos.x);
        transform.position = pos;
        
        // 获得当前角度
        CurAngle = CircularPath.Instance.GetAngle(pos);
        // 法线
        Normal = CircularPath.Instance.GetNormalBuyAngle(CurAngle);
    }

    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="speed">速度</param>
    public void SetMoveSpeed(float speed)
    {
        Speed = speed;
    }
        
   /// <summary>
   /// 启动击飞
   /// </summary>
   /// <param name="flyH">击飞高度</param>
   /// <param name="flyD">击飞距离</param>
   /// <param name="flyS">击飞速度</param>
   public void StartFlyAway(float flyH,float flyD,float flyS)
   {
       if((flyD > 0 && transform.position.x > 2.4f) 
          || (flyD < 0 && transform.position.x < - 2.4f))
           return;
       
       FlyHeight = flyH;
       FlyDis = flyD;
       FlySpeed = flyS;
        
        // 启动击飞
        if (MoveState.Equals("move"))
        {
            // 移动状态
            MoveState = "fly";
            //  击飞角度
            var flyAngle = CircularPath.Instance.LengthToAngle(FlyDis);
            // 结束角度
            var endAngle = CurAngle - flyAngle;
            
            // 开始位置
            StartPos = transform.position;
            // 结束位置
            EndPos = CircularPath.Instance.GetPos(endAngle);
            if (EndPos.x > 2.5f)
            {
                // 极限位置
                EndPos = CircularPath.Instance.GetPosBuyX(2.5f);
                // 获得当前角度
                CurAngle = CircularPath.Instance.GetAngle(EndPos);
            }
            else if (EndPos.x <= -2.5f)
            {
                // 极限位置
                EndPos = CircularPath.Instance.GetPosBuyX(-2.5f);
                // 获得当前角度
                CurAngle = CircularPath.Instance.GetAngle(EndPos);
            }

            // 中间位置
            CenterPos = new Vector3((StartPos.x + EndPos.x) / 2,StartPos.y + FlyHeight);
            // 飞行进度
            FlyProgress = 0;
        }
   }

    // Update is called once per frame
    void Update()
    {
        // 移动
        Move(Time.deltaTime);
        
        // 击飞
        FlyAway(Time.deltaTime);

        // 键盘控制移动
        InputMove();

        // 键盘控制击飞
        InputFlyAway();
        
        if(Input.GetKeyDown(KeyCode.I))
            Init(new Vector3(100,0,0));
    }

    // 移动
    private void Move(float updateTime)
    {
        if(Speed == 0 || !MoveState.Equals("move"))
            return;
        
        CurAngle -= updateTime * Speed;
        var pos = CircularPath.Instance.GetPos(CurAngle);
        // 位置
        transform.position = pos;
        
        // 法线
        Normal = CircularPath.Instance.GetNormalBuyAngle(CurAngle);
    }
    
    // 击飞
    private void FlyAway(float updateTime)
    {
        // 飞行状态中
        if (MoveState.Equals("fly"))
        {
            FlyProgress += FlySpeed * updateTime;
            if (FlyProgress > 1)
                FlyProgress = 1;

            var pos = BezierCurvePos(StartPos, CenterPos, EndPos, FlyProgress);
            // 位置
            transform.position = pos;
             // 获得当前角度
            CurAngle = CircularPath.Instance.GetAngle(pos);
            // 法线
            Normal = CircularPath.Instance.GetNormalBuyAngle(CurAngle);
            // 关闭击飞
            if (FlyProgress >= 1)
            {
                MoveState = "move";
            }
        }
    }
    // 键盘移动移动
    private void InputMove()
    {
        if(!MoveState.Equals("move"))
            return;
        
        var xValue = Input.GetAxis("Horizontal");
        
        if(xValue > 0)
            SetMoveSpeed(100);
        else if(xValue < 0)
            SetMoveSpeed(-100);
        else 
            SetMoveSpeed(0);
    }
    
    // 键盘击飞
    private void InputFlyAway()
    {
        var fly = Input.GetAxis("Jump");

        if (fly > 0)
            StartFlyAway(2, -2, 1);
    }
    
    /// <summary>
    /// 二阶贝塞尔曲线
    /// </summary>
    public static Vector3 BezierCurvePos(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 B = Vector3.zero;
        float t1 = (1 - t) * (1 - t);
        float t2 = 2 * t * (1 - t);
        float t3 = t * t;
        B = t1 * p0 + t2 * p1 + t3 * p2;
        return B;
    }
}
