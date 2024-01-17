using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Transform[] ItemspawnPoint;
    public ItemSpawnData[] ItemspawnData;
    public float levelTime;

    int level;
    float timer;

    void Awake()
    {
        ItemspawnPoint = GetComponentsInChildren<Transform>();  //�ڽĿ�����Ʈ(���� ����Ʈ)��� �ʱ�ȭ
        levelTime = GameManager.instance.maxGameTime / ItemspawnData.Length;    //���� �������� �翡 ���� �ڵ� ���� ������
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), ItemspawnData.Length - 1);

        if (timer > ItemspawnData[level].spawnTime)
        {
            timer = 0;
            ItemSpawn();
        }
    }

    void ItemSpawn()
    {
        GameObject Box = GameManager.instance.pool.Get(5);

        Box.transform.position = ItemspawnPoint[Random.Range(1, ItemspawnPoint.Length)].position; //spawnPoint[1].position;
        Box.GetComponent<Box>().Init(ItemspawnData[level]);
    }
}

[System.Serializable]
public class ItemSpawnData  //���� ����(����ð�, ��������Ʈ, ü��)
{
    public float spawnTime;
    public int spriteType;
    public int helth;
}
