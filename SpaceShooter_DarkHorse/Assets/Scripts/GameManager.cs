using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int wave, level, obstacleAmount, multiplier, boundary;
    public List<GameObject> spaceWreckage;
    public float waveStartDelay, spawnRate, endDelay, levelTime;
    public Text timeText;

    private bool spawningWave, endGame, bossTime;
    // Start is called before the first frame update
    void Start()
    {
        spawningWave = false;
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
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        while(endGame == false && spawningWave == false)
        {//two checks, if a wave spawning is taking affect **and** if the game is over
            StartCoroutine(SpawnWreckageWave());
            if (levelTime <= 0.0f)
            {
                endGame = true;
                wave = 0;
                bossTime = true;
            }
        }
        while(bossTime == true)
        {
            //boss sequence here
        }
        levelTime -= Time.deltaTime;
        UpdateUIComponents();
    }

    private void UpdateUIComponents()
    {
        timeText.text = "Time: " + (int)levelTime / 60 + ":" + (int)levelTime % 60;
        //Debug.Log("Time: " + (int)levelTime / 60 + ":" + (int)levelTime % 60);
    }

    IEnumerator SpawnWreckageWave()
    {
        wave++;
        spawningWave = true;
        obstacleAmount = (int)Mathf.Log(level * wave * multiplier, 2f);
        yield return new WaitForSeconds(waveStartDelay);
        for (int i = 0; i < obstacleAmount; i++)
        {
            Instantiate(spaceWreckage[Random.Range(0, (spaceWreckage.Count))], RandomSpawnPosition(), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
        yield return new WaitForSeconds(endDelay);
        spawningWave = false;
        wave++;
    }

    IEnumerator SpawnEnemies()
    {
        wave++;
        spawningWave = true;
        obstacleAmount = (int)Mathf.Log(level * wave * multiplier, 2f);
        yield return new WaitForSeconds(waveStartDelay);
        for (int i = 0; i < obstacleAmount; i++)
        {
            Instantiate(spaceWreckage[Random.Range(0, (spaceWreckage.Count))], RandomSpawnPosition(), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
        yield return new WaitForSeconds(endDelay);
        spawningWave = false;
        wave++;
    }

    Vector3 RandomSpawnPosition()
    {
        float x = Random.Range(boundary, -boundary);
        return new Vector3(x, 0, 140);
    }
}
