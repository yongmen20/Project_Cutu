using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    int level;
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();  //�ڽĿ�����Ʈ(���� ����Ʈ)��� �ʱ�ȭ
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;    //���� �������� �翡 ���� �ڵ� ���� ������
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);

        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; //spawnPoint[1].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData  //���� ����(����ð�, ��������Ʈ, ü��, �̵��ӵ�)
{
    public float spawnTime;
    public int spriteType;
    public int helth;
    public float speed;
}
