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
            case 0: //��������
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            case 1: //��������
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
            
            case 2: //���ع���
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

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);   // ���������� ���ù� �̿� ����
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

        switch (id) //������ �߻� �ӵ� (���Ÿ� ������ ��� �������� �߻簣�� ����)
        {
            case 0:
                speed = 120;
                Batch();
                break;

            case 1: //�Ķ���
                speed = 3f;   
                break;

            case 2: //����
                speed = 1.5f;
                break;
        }

        player.BroadcastMessage("ApplyGear");   //����, �̵��ӵ� ���� �ȵǴ� ���� ����
    }

    void Batch()    //���� ȸ�� ���� ��ġ
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
                bullet = GameManager.instance.pool.Get(prefabId).transform; //Ǯ �Ŵ������� ������ ��������
                bullet.parent = transform;  //�÷��̾ �θ��
            }

            bullet.localPosition = Vector3.zero;    //���� �ʱ�ȭ
            bullet.localRotation = Quaternion.identity; //ȸ���� ���

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.15f, Space.World);  //1ĭ ���� ��ǥ�� ����, �̵������� ���带 ��������
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero);  //pen(����) -> -100 = ����
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Melee);
        }
    }

    /*
    void Batch2()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;   //ǥ���� ���� ��ǥ

        for (int i = 0; i < count; i++)
        {
            Transform bullet;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform; //Ǯ �Ŵ������� ������ ��������
                bullet.parent = transform;  //�÷��̾ �θ��
            }


            bullet.localPosition = Vector3.zero;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.Translate(bullet.up * 1f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);

        }     
    }
    */

    void Fire() //���� ���Ÿ� ����
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        targetPos.z = 0;    //��������Ʈ ��׷��� ����
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;   //�Ѿ� ����
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }

    void Fire2() //���콺 ���� ����
    {

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;    //��������Ʈ ��׷��� ����
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;   //ǥ���� ���� ��ǥ
        
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir / 1.5f);
        bullet.GetComponent<Bullet>().Init(damage, count, dir * 2.5f);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
