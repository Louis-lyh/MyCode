using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 旋转工具
/// </summary>
public class RotateUnit
{
        /// <summary>
        /// 用某个轴去朝向物体
        /// </summary>
        /// <param name="tr_self">朝向的本体</param>
        /// <param name="lookPos">朝向的目标</param>
        /// <param name="directionAxis">方向轴，取决于你用那个方向去朝向</param>
        void AxisLookAt(Transform tr_self, Vector3 lookPos, Vector3 directionAxis)
        {
            var rotation = tr_self.rotation;
            var targetDir = lookPos - tr_self.position;
            
            //指定哪根轴朝向目标,自行修改Vector3的方向
            var fromDir = tr_self.rotation * directionAxis;
            
            //计算垂直于当前方向和目标方向的轴
            var axis = Vector3.Cross(fromDir, targetDir).normalized;
            
            //计算当前方向和目标方向的夹角
            var angle = Vector3.Angle(fromDir, targetDir);
            
            //将当前朝向向目标方向旋转一定角度，这个角度值可以做插值
            tr_self.rotation = Quaternion.AngleAxis(angle, axis) * rotation;
        }
        
        /// <summary>
        /// 用某个轴去朝向指定方向
        /// </summary>
        /// <param name="rotation">自身旋转</param>
        /// <param name="targetDir">目标方向</param>
        /// <param name="directionAxis">指定的轴</param>
        /// <returns></returns>
        Quaternion AxisLookAt(Quaternion rotation,Vector3 targetDir, Vector3 directionAxis)
        {
            //指定哪根轴朝向目标,自行修改Vector3的方向
            var fromDir = rotation * directionAxis;
            
            //计算垂直于当前方向和目标方向的轴
            var axis = Vector3.Cross(fromDir, targetDir).normalized;
            
            //计算当前方向和目标方向的夹角
            var angle = Vector3.Angle(fromDir, targetDir);

            return Quaternion.AngleAxis(angle, axis) * rotation;
        }
    
    
    /// <summary>
    /// 2d跟踪 x轴指向目标
    /// </summary>
    /// <param name="self">旋转本体</param>
    /// <param name="target">旋转目标</param>
    void LookAtUnit2D(Transform self,Vector3 target)
    {
        var dir = target - self.position;
        // 世界坐标系下的角度
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // 旋转后的四元素
        var newRotate = Quaternion.AngleAxis(angle, Vector3.forward);
        // 旋转z轴 使x轴对齐方向
        self.rotation = newRotate;
    }
}
