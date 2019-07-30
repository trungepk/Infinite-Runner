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
    [SerializeField] private int coinsEachLine = 5;
    [SerializeField] [Range(.2f, .7f)] private float distanceBetweenCoins = .5f;
    

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
        string[] typesOfObstacle = { Constants.ObstacleTag, Constants.TallObstacleTag, Constants.HighObstacleTag };
        int rnd = Random.Range(0, typesOfObstacle.Length);
        GameObject obstacle = ObjectPooler.SharedInstance.GetPooledObject(typesOfObstacle[rnd]);
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
            StartCoroutine(SpawnCoinLine());
            coinSpawnRate = Random.Range(minCoinSpawnRate, maxCoinSpawnRate);
        }
        coinSpawnRate -= Time.deltaTime;
    }

    private IEnumerator SpawnCoinLine()
    {
        for(var i = 0; i < coinsEachLine; i++)
        {
            SpawnCoin();
            yield return new WaitForSeconds(distanceBetweenCoins);
        }
    }

    private void SpawnCoin()
    {
        GameObject coin = ObjectPooler.SharedInstance.GetPooledObject(Constants.CoinTag);
        if (coin)
        {
            coin.transform.position = transform.position;
            coin.transform.rotation = Quaternion.identity;
            coin.SetActive(true);
        }
    }
}
