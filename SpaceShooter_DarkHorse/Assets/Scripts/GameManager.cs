using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class GameManager : MonoBehaviour
{
    public int boundaryTop, boundaryBot, wave, level, obstacleAmount, enemyAmount, obstacleMultiplier, enemyMultiplier;
    public List<GameObject> spaceWreckage, enemyTypes;
    public float waveStartDelay, spawnRate, endDelay, levelTime;
    public Text timeText;
    public GameObject menu, player, boss;

    private bool spawningObstacleWave, spawningEnemyWave, endGame, bossTime, levelActive;
    // Start is called before the first frame update
    public void Awake()
    {
        GameAnalytics.Initialize();
        //GameAnalytics.NewResourceEvent(GAResourceFlowType, “PlayerHealth”, 400, “playerstat”, “playerh”);
        //GameAnalytics.NewProgressionEvent(GA_Progression.GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03, int score);
        //GameAnalytics.NewDesignEvent(string eventName, float eventValue);

        levelActive = false;//need to flip when ready
        spawningObstacleWave = false;
        endGame = false;
        bossTime = false;
        wave = 1;
        obstacleMultiplier = 20;
        enemyMultiplier = 10;
        obstacleAmount = 0;
        enemyAmount = 0;
        LevelSpecificValues();
        UpdateUIComponents();
        level = 1;
        //Instantiate(player, transform.position, Quaternion.identity);
        //Instantiate(boss);

        player.gameObject.SetActive(false);
        boss.gameObject.SetActive(false);
    }
    public void Update()
    {
        //Debug.Log("Level Activce");
        LevelSequence();
    }


    public void LevelSequence()
    {
        if (levelActive == true)
        {//while a level is active
            if (endGame == false)
            {//two checks, if a wave spawning is taking affect **and** if the game is over
                if (spawningObstacleWave == false && spawningEnemyWave == false)
                {
                    StartCoroutine(SpawnWreckageWave());
                    StartCoroutine(SpawnEnemies());
                    wave++;//if both routines are done, wave increase
                }
                if (levelTime <= 0.0f)
                {
                    endGame = true;
                    wave = 0;
                    bossTime = true;//move onto boss phase
                    //GameObject.FindWithTag("Boss").GetComponent<BossBehaviour>().ResetBoss();
                    boss.SetActive(true);
                    boss.GetComponent<BossBehaviour>().ResetBoss();
                }
            }
            else if (bossTime == true)
            {
                //boss sequence here
                if (spawningObstacleWave == false && spawningEnemyWave == false && level == 3)
                {//spawn obstacle and enemies again but with a boss
                    StartCoroutine(SpawnWreckageWave());
                    StartCoroutine(SpawnEnemies());
                    wave++;//if both routines are done, wave increase
                }
                //if boss is dead, destory all enemies
                if (/*GameObject.Find("Boss")*/boss.GetComponent<BossBehaviour>().isBossDead() == true)
                {
                    bossTime = false;
                    levelActive = false;
                    menu.SetActive(true);
                    boss.SetActive(false);
                    player.gameObject.SetActive(false);
                }
            }
            levelTime -= Time.deltaTime;
            UpdateUIComponents();
            if(player.gameObject.GetComponent<PlayerController>().IsDead() == true)
            {
                endGame = true;
                wave = 0;
                bossTime = true;//move onto boss phase
                                //GameObject.FindWithTag("Boss").GetComponent<BossBehaviour>().ResetBoss();
                bossTime = false;
                levelActive = false;
                menu.SetActive(true);
                boss.SetActive(false);
                player.gameObject.SetActive(false);
            }
        }//active loop end
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level End");
    }

    private void LevelSpecificValues()
    {
        switch (level)
        {
            case 1:
                levelTime = 30f;//2 mins
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level 1");
                break;
            case 2:
                levelTime = 180f;//3 mins
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level 2");
                break;
            case 3:
                levelTime = 240f;//4 mins
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level 3");
                break;
            default:
                //will be default (possible level 1 difficulty)
                levelTime = 120f;//2 mins
                break;
        }
    }

    private void UpdateUIComponents()
    {
        if(bossTime == true)
        {
            timeText.text = "Time: BossTime!";
        }
        else
        {
            timeText.text = "Time: " + (int)levelTime / 60 + ":" + (int)levelTime % 60;
        }
    }

    IEnumerator SpawnWreckageWave()
    {
        spawningObstacleWave = true;
        obstacleAmount = (int)Mathf.Log(level * wave * obstacleMultiplier, 3f);//create obstale amount
        yield return new WaitForSeconds(waveStartDelay);
        for (int i = 0; i < obstacleAmount; i++)
        {
            Instantiate(spaceWreckage[Random.Range(0, (spaceWreckage.Count))], RandomSpawnPosition(), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
        yield return new WaitForSeconds(endDelay);
        spawningObstacleWave = false;
    }

    IEnumerator SpawnEnemies()
    {//spawn enemies
        spawningEnemyWave = true;
        enemyAmount = (int)Mathf.Log(level * wave * enemyMultiplier, 2f);
        yield return new WaitForSeconds(waveStartDelay);
        for (int i = 0; i < enemyAmount; i++)
        {
            Instantiate(enemyTypes[Random.Range(0, (enemyTypes.Count))], RandomSpawnPosition(), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
        yield return new WaitForSeconds(endDelay);
        spawningEnemyWave = false;
    }

    Vector3 RandomSpawnPosition()
    {
        float z = Random.Range(boundaryTop, boundaryBot);
        return new Vector3(180, 0, z);
    }

    public void MenuOptions(int inputLevel)
    {
        //level options
        level = inputLevel;
        ResetLevel();
    }

    public int GetLevel()
    {
        return level;
    }

    public void ResetLevel()
    {
        menu.SetActive(false);
        LevelSpecificValues();
        levelActive = true;
        spawningObstacleWave = false;
        endGame = false;
        bossTime = false;
        wave = 1;
        UpdateUIComponents();
        player.GetComponent<PlayerController>().InitializeLevelStats();
        player.gameObject.SetActive(true);
    }

    public void PlayerShootCall()
    {
        player.gameObject.GetComponent<PlayerController>().ShootTouch();
        //for testing puproses
        //StartCoroutine(SpawnWreckageWave());
        //StartCoroutine(SpawnEnemies());
        //wave++;//if both routines are done, wave increase
        //if (spawningObstacleWave == false && spawningEnemyWave == false)
        //{
        //}

    }
}
