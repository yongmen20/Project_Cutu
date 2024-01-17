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

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))    //������ ����X
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;  //�������� �ӵ� ����(�΋Hĥ �� ƨ��� ����)
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive)
            return;

        spriter.flipX = target.position.x > rigid.position.x;
    }

    void OnEnable() //�׾��� �� �ǻ츮��
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        helth = maxHelth;   //�ٽ� ������� �� ü�� �ʱ�ȭ
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHelth = data.helth;
        helth = data.helth;
    }

    void OnTriggerEnter2D(Collider2D collision) //����� �浹�� 
    {
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }

        if (!collision.CompareTag("Bullet") || !isLive) //���� ������� ��� ���� �߰�(�߿�)
            return;

        helth -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        // = StartCoroutine("KnockBack"); �̷� ������ �ۼ��ص� ������

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
        yield return wait;  //���� �ϳ��� ���� ������ ���
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 0.15f, ForceMode2D.Impulse);  //�˹鿡 �������� ��

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
