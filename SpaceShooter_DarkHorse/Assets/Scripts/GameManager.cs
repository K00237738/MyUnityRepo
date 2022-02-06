using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level, obstacleAmount, multiplier, boundary;
    public List<GameObject> spaceWreckage;
    public float waveStartDelay, spawnRate, endDelay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWreckageWave());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnWreckageWave()
    {
        obstacleAmount = (int)Mathf.Log(level*multiplier, 2f);
        yield return new WaitForSeconds(waveStartDelay);
        for (int i = 0; i < obstacleAmount; i++)
        {
            Instantiate(spaceWreckage[Random.Range(0, (spaceWreckage.Count-1))], RandomSpawnPosition(), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
        yield return new WaitForSeconds(endDelay);
    }

    Vector3 RandomSpawnPosition()
    {
        float x = Random.Range(boundary, -boundary);
        return new Vector3(x, 0, 140);
    }
}
