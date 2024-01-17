using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (id)
        {
            case 0: //근접무기
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            case 1: //유도무기
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
            
            case 2: //조준무기
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire2();
                }
                break;

            /*
            case 3:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Batch2();
                }
                break;
            */
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);   // 무조건적인 리시버 이용 방지
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for(int i= 0; i < GameManager.instance.pool.prefabs.Length; i++)
        {
            if(data.projectile == GameManager.instance.pool.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        switch (id) //무기의 발사 속도 (원거리 무기의 경우 작을수록 발사간격 감소)
        {
            case 0:
                speed = 120;
                Batch();
                break;

            case 1: //파랑새
                speed = 3f;   
                break;

            case 2: //유성
                speed = 1.5f;
                break;
        }

        player.BroadcastMessage("ApplyGear");   //공속, 이동속도 적용 안되는 오류 방지
    }

    void Batch()    //근접 회전 무기 배치
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i); 
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform; //풀 매니저에서 프리팹 가져오기
                bullet.parent = transform;  //플레이어를 부모로
            }

            bullet.localPosition = Vector3.zero;    //백터 초기화
            bullet.localRotation = Quaternion.identity; //회전량 계산

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.15f, Space.World);  //1칸 위의 좌표에 생성, 이동방향은 월드를 기준으로
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero);  //pen(관통) -> -100 = 무한
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Melee);
        }
    }

    /*
    void Batch2()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;   //표적에 대한 좌표

        for (int i = 0; i < count; i++)
        {
            Transform bullet;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform; //풀 매니저에서 프리팹 가져오기
                bullet.parent = transform;  //플레이어를 부모로
            }


            bullet.localPosition = Vector3.zero;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.Translate(bullet.up * 1f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);

        }     
    }
    */

    void Fire() //유도 원거리 공격
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        targetPos.z = 0;    //스프라이트 찌그러짐 방지
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;   //총알 생성
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }

    void Fire2() //마우스 조준 공격
    {

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;    //스프라이트 찌그러짐 방지
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;   //표적에 대한 좌표
        
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir / 1.5f);
        bullet.GetComponent<Bullet>().Init(damage, count, dir * 2.5f);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
