using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float ghostDelay;    //딜레이 시간
    public float ghostDelaySeconds; //시간 적용
    public GameObject ghost;    //잔상 스프라이트
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
                ghostDelaySeconds -= Time.deltaTime;    //생성딜레이시간 흐름
            }
            else
            {
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);   //현재 스프라이트 정보 가져오기
                if(turnCheck == true )  //turnChck를 통해 이전에 플레이어의 방향을 파악해 flipX를 적용하거나 안함***
                {
                    currentGhost.GetComponent<SpriteRenderer>().flipX = true;
                }
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;   //현재의 스프라이트 정보 가져오기             
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite; //프레임당 스프라이트 애니메이션 적용 

                if (player.inputVec.x != 0)  //플에이어가 x축 이동을 시행했을 때
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
