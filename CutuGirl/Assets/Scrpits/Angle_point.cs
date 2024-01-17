using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle_point : MonoBehaviour
{
    public Transform pos;

    void Update()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;    //스프라이트 찌그러짐 방지
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;   //표적에 대한 좌표

        pos.rotation = Quaternion.FromToRotation(Vector3.up, dir / 1.5f);
    }
}
