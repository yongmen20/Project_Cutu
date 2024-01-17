using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public float helth;
    public float maxHelth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;
    public GameObject Exp;
    public GameObject Coin;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))    //죽으면 실행X
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;  //물리적인 속도 제거(부딫칠 때 튕기기 방지)
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive)
            return;

        spriter.flipX = target.position.x > rigid.position.x;
    }

    void OnEnable() //죽었던 적 되살리기
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        helth = maxHelth;   //다시 재생성될 때 체력 초기화
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHelth = data.helth;
        helth = data.helth;
    }

    void OnTriggerEnter2D(Collider2D collision) //무기와 충돌시 
    {
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }

        if (!collision.CompareTag("Bullet") || !isLive) //적이 사망했을 경우 조건 추가(중요)
            return;

        helth -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        // = StartCoroutine("KnockBack"); 이런 식으로 작성해도 동작함

        if (helth > 0)  //적 체력 남음
        {
            anim.SetTrigger("Hit"); //Hit 애니메이션 재생
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else    //적 사망
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 2;
            anim.SetBool("Dead", true);
            int drop = Random.RandomRange(0, 5);
            if (drop == 4)
                DropCoin();
            else
                DropExp();
            GameManager.instance.kill++;

            if(GameManager.instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;  //다음 하나의 물리 프레임 대기
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 0.15f, ForceMode2D.Impulse);  //넉백에 가해지는 힘

    }

    void DropExp()
    {
        if (!GameManager.instance.isLive)
            return;

        Exp = GameManager.instance.pool.Get(4);
        Exp.transform.position = transform.position;
    }

    void DropCoin()
    {
        if (!GameManager.instance.isLive)
            return;
        Coin = GameManager.instance.pool.Get(7);
        Coin.transform.position = transform.position;
    }
}
