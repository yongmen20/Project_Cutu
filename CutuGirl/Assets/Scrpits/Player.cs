using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("이동")]
    [SerializeField] public Vector2 inputVec;
    [SerializeField] public float speed;

    [Header("대쉬")]
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashLength;
    [SerializeField] public float dashCooldown;
    bool isDashing;
    public bool canDash = true;

    public Ghost ghost;

    [Header("스캔")]
    [SerializeField] public Scanner scanner;

    Rigidbody2D rb;
    SpriteRenderer spriter;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();

        canDash = true;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        if (isDashing) return;

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Tel_Sound);
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (isDashing) return;

        spriter.color = new Color(1, 1, 1, 1);
        rb.velocity = inputVec.normalized * speed;
        //Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + nextVec);
    }

    private void LateUpdate()   //캐릭터 x축 회전
    {
        if (!GameManager.instance.isLive)
            return;

        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0; //true 혹은 false로 회전 여부 구현
        }
    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        ghost.makeGhost = true;
        rb.velocity = inputVec.normalized * dashSpeed;
        yield return new WaitForSeconds(dashLength);
        isDashing = false;
        ghost.makeGhost = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive || !collision.gameObject.CompareTag("Enemy"))
            return;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Player_Hit);
        spriter.color = new Color(1, 1, 1, 0.4f);
    }


    private void OnCollisionStay2D(Collision2D collision)   //플레이어 데미지 처리(충돌확인)
    {
        if (!GameManager.instance.isLive || !collision.gameObject.CompareTag("Enemy"))
            return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health < 0)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
