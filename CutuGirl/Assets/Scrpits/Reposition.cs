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
        if (!collision.CompareTag("Area"))  //�÷��̾� ������ �簢�� �ݶ��̴�(ī�޶� ũ��� ���� �ϱ�)
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
                    transform.Translate((dist * 3) + ran);    //�� ���� ����
                }
                break;

            case "Box":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
                    transform.Translate((dist * 3) + ran);    //�� ���� ����
                }
                break;
        }
    }
}
