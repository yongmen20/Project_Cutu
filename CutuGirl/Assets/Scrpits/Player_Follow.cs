using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Follow : MonoBehaviour
{
    public float Speed;
    public Transform player;
    public bool ReadyToMove;

    void Update()
    {
        if(ReadyToMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ReadyToMove = true;
            player = GameObject.FindWithTag("Player").transform;
        }
    }

}
