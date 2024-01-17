using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HPUP : MonoBehaviour
{
    public GameObject parent;

    public Player_Follow Hp;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision) //경험치와 충돌 시
    {
        if (!collision.CompareTag("Player"))
            return;

        if(GameManager.instance.health >= GameManager.instance.maxHealth / 3)
            GameManager.instance.health = GameManager.instance.maxHealth;
        
        else 
            GameManager.instance.health += GameManager.instance.maxHealth / 3;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Heal);
        Hp.ReadyToMove = false;
        parent.gameObject.SetActive(false);
    }

}