using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    public float helth;
    public float maxHelth;
    public RuntimeAnimatorController[] animCon;

    bool isLive;
    public GameObject Hp;

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

        rigid.velocity = Vector2.zero;  //물리적인 속도 제거(부딫칠 때 튕기기 방지)
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive)
            return;
    }

    void OnEnable() //죽었던 적 되살리기
    {
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        helth = maxHelth;   //다시 재생성될 때 체력 초기화
    }

    public void Init(ItemSpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        maxHelth = data.helth;
        helth = data.helth;
    }

    void OnTriggerEnter2D(Collider2D collision) //무기와 충돌시 
    {
        if (!collision.CompareTag("Bullet") || !isLive) //적이 사망했을 경우 조건 추가(중요)
            return;

        helth -= collision.GetComponent<Bullet>().damage;
        StartCoroutine("KnockBack");

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
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            DropHp();

            if (GameManager.instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;  //다음 하나의 물리 프레임 대기
    }

    void DropHp()
    {
        if (!GameManager.instance.isLive)
            return;

        Hp = GameManager.instance.pool.Get(6);
        Hp.transform.position = transform.position;
    }
}
