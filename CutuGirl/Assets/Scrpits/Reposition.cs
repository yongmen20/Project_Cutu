using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))  //플레이어 주위의 사각형 콜라이더(카메라 크기와 같게 하기)
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        switch (transform.tag)
        {
            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
                    transform.Translate((dist * 3) + ran);    //적 스폰 범위
                }
                break;

            case "Box":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
                    transform.Translate((dist * 3) + ran);    //적 스폰 범위
                }
                break;
        }
    }
}
