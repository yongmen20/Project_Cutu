using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public LayerMask targetLayer2;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);    //CircleCastAll : 원형태로 스캔, (캐스팅 시작위치, 원의 반지름, 캐스팅 방향, 캐스팅 길이, 대상 레이어)
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curdiff = Vector3.Distance(myPos, targetPos);

            if (curdiff < diff)
            {
                diff = curdiff;
                result = target.transform;
            }
        }

        return result;
    }

}
