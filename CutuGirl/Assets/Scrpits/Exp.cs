using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public GameObject parent;

    public Player_Follow EXp;
    Animator anim;
   
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D collision) //경험치와 충돌 시
    {
        if (!collision.CompareTag("Player"))
            return;

        GameManager.instance.GetExp();

        EXp.ReadyToMove = false;
        parent.gameObject.SetActive(false);
    }
}
