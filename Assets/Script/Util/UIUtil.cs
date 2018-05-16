using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Util
{
    public static class UIUtil
    {
        /// <summary>
        /// 适用于在平行于相机平面内移动目标
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetTargetPlaneCursor(Camera camera,GameObject target,Vector2 screenPoint)
        {
            Vector3 direct = camera.ScreenToWorldPoint(new Vector3(screenPoint.x,screenPoint.y,1)).normalized;
            Vector3 axis = target.transform.position - camera.transform.position;
            float length = axis.magnitude/ Mathf.Cos(Vector3.Angle(direct, axis));
            direct = direct * length;
            return direct - axis;
        }
    }
}
