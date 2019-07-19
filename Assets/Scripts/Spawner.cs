using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private float obstacleSpawnRate;
    [SerializeField] private float minObstacleSpawnRate = 10f;
    [SerializeField] private float maxObstacleSpawnRate = 50f;

    [SerializeField] private bool isSpawnCoin;
    private float coinSpawnRate;
    [SerializeField] private float minCoinSpawnRate = 10f;
    [SerializeField] private float maxCoinSpawnRate = 50f;
    

	void Start () {
        obstacleSpawnRate = Random.Range(minObstacleSpawnRate, maxObstacleSpawnRate);
        coinSpawnRate = Random.Range(minCoinSpawnRate, maxCoinSpawnRate);
    }
	
	void Update ()
    {
        if (isSpawnCoin)
        {
            CountDownToSpawnCoin();
            return;
        }
        CountDownToSpawnObstacle();
    }

    private void CountDownToSpawnObstacle()
    {
        if (obstacleSpawnRate <= 0)
        {
            SpawnObstacle();
            obstacleSpawnRate = Random.Range(minObstacleSpawnRate, maxObstacleSpawnRate);
        }
        obstacleSpawnRate -= Time.deltaTime;
    }

    private void SpawnObstacle()
    {
        GameObject obstacle = ObjectPooler.SharedInstance.GetPooledObject("Obstacle");
        if (obstacle)
        {
            obstacle.transform.position = transform.position;
            obstacle.transform.rotation = Quaternion.identity;
            obstacle.SetActive(true);
        }
    }

    private void CountDownToSpawnCoin()
    {
        if (coinSpawnRate <= 0)
        {
            SpawnCoin();
            coinSpawnRate = Random.Range(minCoinSpawnRate, maxCoinSpawnRate);
        }
        coinSpawnRate -= Time.deltaTime;
    }

    private void SpawnCoin()
    {
        GameObject obstacle = ObjectPooler.SharedInstance.GetPooledObject("Coin");
        if (obstacle)
        {
            obstacle.transform.position = transform.position;
            obstacle.transform.rotation = Quaternion.identity;
            obstacle.SetActive(true);
        }
    }
}
