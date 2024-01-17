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
        spawnPoint = GetComponentsInChildren<Transform>();  //자식오브젝트(스폰 포인트)들로 초기화
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;    //몬스터 데이터의 양에 따라 자동 레벨 디자인
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
public class SpawnData  //스폰 정보(등장시간, 스프라이트, 체력, 이동속도)
{
    public float spawnTime;
    public int spriteType;
    public int helth;
    public float speed;
}
