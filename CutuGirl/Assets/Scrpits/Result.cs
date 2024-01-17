using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public GameObject[] titles;   //GameResult에서 졌을때(0), 이겼을때(1) 제목 넣기
    public enum InfoType { Result }
    public InfoType type;

    Text myText;

    private void Awake()
    {
        myText = GetComponent<Text>();
    }
    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Result:
                myText.text = string.Format("KILLSCORE:{0:F0}", GameManager.instance.kill * 10);
                break;
        }
    }

        public void Lose()
    {
        //titles[0].SetActive(true);
    }

    public void Win()
    {
        //titles[1].SetActive(true);
    }
}
