using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Gear: " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    // ������ ȿ�� �ο�
    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Gear:
                RateUp();
                break;
            case ItemData.ItemType.Dash:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons)
        {
            switch(weapon.id) 
            {
                case 0:     //��ũ�γ����
                    weapon.speed = 120 + (120 * rate); 

                    break;

                case 1:     //�Ķ���
                    weapon.speed = 3f * (1f - rate);
                    break;

                case 2:     //����
                    weapon.speed = 1.5f * (1f - rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 1.3f; //�÷��̾��� ���� ���ǵ�
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
