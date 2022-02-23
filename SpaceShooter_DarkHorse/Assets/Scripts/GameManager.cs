using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int wave, level, obstacleAmount, enemyAmount, obstacleMultiplier, enemyMultiplier, boundary;
    public List<GameObject> spaceWreckage, enemyTypes;
    public float waveStartDelay, spawnRate, endDelay, levelTime;
    public Text timeText;

    private bool spawningObstacleWave, spawningEnemyWave, endGame, bossTime, levelActive;
    // Start is called before the first frame update
    void Start()
    {
        levelActive = true;
        spawningObstacleWave = false;
        endGame = false;
        bossTime = false;
        wave = 0;
        LevelSpecificValues();
        UpdateUIComponents();
    }

    private void LevelSpecificValues()
    {
        switch(level)
        {
            case 1:
                levelTime = 120f;//2 mins
                break;
            case 2:
                levelTime = 180f;//3 mins
                break;
            case 3:
                levelTime = 240f;//4 mins
                break;
            default:
                //will be default (possible level 1 difficulty)
                levelTime = 120f;//2 mins
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(levelActive)
        {//while a level is active
            if(endGame == false/* && spawningObstacleWave == false && spawningEnemyWave == false*/)
            {//two checks, if a wave spawning is taking affect **and** if the game is over
                if(spawningObstacleWave == false && spawningEnemyWave == false)
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
                }
            }
            else if(bossTime == true)
            {
                //boss sequence here
                if (spawningObstacleWave == false && spawningEnemyWave == false && level == 3)
                {//spawn obstacle and enemies again but with a boss
                    StartCoroutine(SpawnWreckageWave());
                    StartCoroutine(SpawnEnemies());
                    wave++;//if both routines are done, wave increase
                }
                //if boss is dead, destory all enemies

            }
            levelTime -= Time.deltaTime;
            UpdateUIComponents();
        }
    }

    private void UpdateUIComponents()
    {
        timeText.text = "Time: " + (int)levelTime / 60 + ":" + (int)levelTime % 60;
        //Debug.Log("Time: " + (int)levelTime / 60 + ":" + (int)levelTime % 60);
    }

    IEnumerator SpawnWreckageWave()
    {
        spawningObstacleWave = true;
        obstacleMultiplier = 20;
        obstacleAmount = (int)Mathf.Log(level * wave * obstacleMultiplier, 2f);//create obstale amount
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
        enemyMultiplier = 10;
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
        float x = Random.Range(boundary, -boundary);
        return new Vector3(x, 0, 140);
    }

    void BossDead()
    {
        //boss is dead
    }
}
