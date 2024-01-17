using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSetting : MonoBehaviour
{
    SpriteRenderer spriter;

    void Start()
    {
        spriter = GetComponent<SpriteRenderer>();
        spriter.sortingOrder = 4;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriter.color = new Color(1, 1, 1, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriter.color = new Color(1, 1, 1, 1f);
        }
    }
}
