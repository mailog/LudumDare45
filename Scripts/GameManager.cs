using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mouseEffect;

    public bool levelComplete;

    public bool started;

    public Image fadeScreen;

    public float fadeCounter, fadeTime;

    public bool firstLevel, lastLevel;

    public KillCounter killCount;

    public bool isGameOver;

    public GameObject gameOverScreen;

    public GameObject victoryScreen;

    public NakedHero hero;

    public EnemySpawner spawner;

    public int totalEnemies;

    public Text enemyCount;

    public Text gameOverCount;
    
    // Start is called before the first frame update
    void Start()
    {
        killCount = GameObject.FindWithTag("KillCounter").GetComponent<KillCounter>();
        spawner.enemyTotal = totalEnemies;
        enemyCount.text = totalEnemies + " ENEMIES LEFT";
    }

    // Update is called once per frame
    void Update()
    {
        if(!started)
        {
            if(FadeIn())
                LevelStart();
        }
        else if(levelComplete)
        {
            if(lastLevel)
            {
                GameComplete();
            }
            else
            {
                if (FadeOut())
                    NextLevel();
            }
        }

        CheckMouseClick();
    }

    void CheckMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Click");
            Instantiate(mouseEffect, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }
    }

    void MouseClick()
    {

    }

    public bool FadeIn()
    {
        fadeCounter += Time.deltaTime;
        fadeScreen.color = Color.Lerp(Color.black, Color.clear, fadeCounter / fadeTime);
        return (fadeCounter / fadeTime) >= 1;
    }

    public bool FadeOut()
    {
        fadeCounter -= Time.deltaTime;
        fadeScreen.color = Color.Lerp(Color.black, Color.clear, fadeCounter / fadeTime);
        return (fadeCounter / fadeTime) <= 0;
    }

    public void LevelStart()
    {
        fadeScreen.gameObject.SetActive(false);
        started = true;
        hero.enabled = true;
        spawner.enabled = true;
    }

    public void EnemyDeath()
    {
        killCount.AddKill();
        totalEnemies--;
        if(totalEnemies < 0)
        {
            totalEnemies = 0;
        }
        enemyCount.text = totalEnemies + " ENEMIES LEFT";
        if(totalEnemies <= 0)
        {
            fadeScreen.gameObject.SetActive(true);
            levelComplete = true;
        }
    }
    
    void ChooseNext()
    {
        if(lastLevel)
        {
            GameComplete();
        }
        else
        {
            NextLevel();
        }
    }

    public void GameComplete()
    {
        hero.enabled = false;
        victoryScreen.SetActive(true);
    }

    public void GameOver()
    {
        isGameOver = true;
        spawner.enabled = false;
        gameOverScreen.SetActive(true);
        gameOverCount.text = killCount.GetKills() + " Enemies Slain";
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {

        SceneManager.LoadScene(0);
    }
}
