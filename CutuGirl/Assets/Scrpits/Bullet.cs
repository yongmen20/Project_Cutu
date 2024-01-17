using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int pen;

    Animator anim;
    Rigidbody2D rigid;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int pen, Vector3 dir)
    {
        this.damage = damage;
        this.pen = pen;

        if (pen >= 0)
        {
            rigid.velocity = dir * 3f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) //물리충돌시 작동
    {
        if (!collision.CompareTag("Enemy") || pen == -100)
            return;

        pen--;

        if (pen < 0)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }

        if (!collision.CompareTag("Area") || pen == -100)
            return;

        gameObject.SetActive(false);
    }
}
