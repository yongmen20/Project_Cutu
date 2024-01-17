using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float ghostDelay;    //������ �ð�
    public float ghostDelaySeconds; //�ð� ����
    public GameObject ghost;    //�ܻ� ��������Ʈ
    public bool makeGhost = false;
    bool turnCheck = false;

    Player player;
    SpriteRenderer spriter;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();

        ghostDelaySeconds = ghostDelay;
    }

    private void Update()
    {
        if (makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;    //���������̽ð� �帧
            }
            else
            {
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);   //���� ��������Ʈ ���� ��������
                if(turnCheck == true )  //turnChck�� ���� ������ �÷��̾��� ������ �ľ��� flipX�� �����ϰų� ����***
                {
                    currentGhost.GetComponent<SpriteRenderer>().flipX = true;
                }
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;   //������ ��������Ʈ ���� ��������             
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite; //�����Ӵ� ��������Ʈ �ִϸ��̼� ���� 

                if (player.inputVec.x != 0)  //�ÿ��̾ x�� �̵��� �������� ��
                {
                    currentGhost.GetComponent<SpriteRenderer>().flipX = player.inputVec.x < 0;
                    turnCheck = true;
                }

                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 0.2f);
            }
        }
    }
}
