using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject parent;

    public Player_Follow COin;
    Animator anim;
   
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D collision) //동전과 충돌 시
    {
        if (!collision.CompareTag("Player"))
            return;

        GameManager.instance.GetCoin();

        COin.ReadyToMove = false;
        parent.gameObject.SetActive(false);
    }
}
