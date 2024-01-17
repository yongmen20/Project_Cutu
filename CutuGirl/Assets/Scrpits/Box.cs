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

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))    //������ ����X
            return;

        rigid.velocity = Vector2.zero;  //�������� �ӵ� ����(�΋Hĥ �� ƨ��� ����)
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive)
            return;
    }

    void OnEnable() //�׾��� �� �ǻ츮��
    {
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        helth = maxHelth;   //�ٽ� ������� �� ü�� �ʱ�ȭ
    }

    public void Init(ItemSpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        maxHelth = data.helth;
        helth = data.helth;
    }

    void OnTriggerEnter2D(Collider2D collision) //����� �浹�� 
    {
        if (!collision.CompareTag("Bullet") || !isLive) //���� ������� ��� ���� �߰�(�߿�)
            return;

        helth -= collision.GetComponent<Bullet>().damage;
        StartCoroutine("KnockBack");

        if (helth > 0)  //�� ü�� ����
        {
            anim.SetTrigger("Hit"); //Hit �ִϸ��̼� ���
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else    //�� ���
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
        yield return wait;  //���� �ϳ��� ���� ������ ���
    }

    void DropHp()
    {
        if (!GameManager.instance.isLive)
            return;

        Hp = GameManager.instance.pool.Get(6);
        Hp.transform.position = transform.position;
    }
}
