using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int wave, level, obstacleAmount, multiplier, boundary;
    public List<GameObject> spaceWreckage;
    public float waveStartDelay, spawnRate, endDelay;
    private bool spawningWave, endGame;
    // Start is called before the first frame update
    void Start()
    {
        spawningWave = false;
        endGame = false;
        wave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        while(endGame == false && spawningWave == false)
        {//two checks, if a wave spawning is taking affect **and** if the game is over
            StartCoroutine(SpawnWreckageWave());
            if (wave == 5)
            {
                endGame = true;
                wave = 0;
            }
        }
    }

    IEnumerator SpawnWreckageWave()
    {
        wave++;
        spawningWave = true;
        obstacleAmount = (int)Mathf.Log(level * wave * multiplier, 2f);
        yield return new WaitForSeconds(waveStartDelay);
        for (int i = 0; i < obstacleAmount; i++)
        {
            Instantiate(spaceWreckage[Random.Range(0, (spaceWreckage.Count-1))], RandomSpawnPosition(), Quaternion.identity);
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
