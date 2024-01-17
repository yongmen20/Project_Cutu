using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime;

    [Header("# Player Info")]
    public float health;
    public float maxHealth = 70;
    public int level;
    public int kill;
    public int coin;
    public int exp;
    public int[] nextExp;

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;
    public GameObject MenuSet;

    public Ghost ghost;
    WaitForFixedUpdate wait;


    void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;
        uiLevelUp.Select(2);
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false; //모든 오브젝트 행동 정지
        StopCoroutine(player.Dash());

        yield return new WaitForSeconds(0.5f);  //사망 애니메이션 길이만큼

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();

        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false; //모든 오브젝트 행동 정지
        enemyCleaner.SetActive(true);
        StopCoroutine(player.Dash());

        yield return new WaitForSeconds(1.5f); //적 처치 애니메이션 길이만큼

        uiResult.gameObject.SetActive(true);
        uiResult.Win();

        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);   //LoadScene(씬 이름 or 번호)
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (!isLive)
            return;

        if (Input.GetButtonDown("Cancel"))
        {
            if (MenuSet.activeSelf)
            {
                Time.timeScale = 1;
                MenuSet.SetActive(false);
                AudioManager.instance.EffectBgm(false);
            }
            else
            {
                Time.timeScale = 0;
                MenuSet.SetActive(true);
                AudioManager.instance.EffectBgm(true);
            }
        }


        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive)
            return;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.EXP);
        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void GetCoin()
    {
        if (!isLive)
            return;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Coin);
        coin++;
            
        // 코인 내역 저장 함수
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0; //timeScale : 유니티 시간 속도(배율), 1은 기본 속도
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
