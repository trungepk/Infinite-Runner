using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private float spawnRate;
    [SerializeField] private float minSpawnRate = 10f;
    [SerializeField] private float maxSpawnRate = 50f;
    

	void Start () {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }
	
	void Update ()
    {
        CountDownToSpawn();
    }

    private void CountDownToSpawn()
    {
        if (spawnRate <= 0)
        {
            Spawn();
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
        spawnRate -= Time.deltaTime;
    }

    private void Spawn()
    {
        GameObject obstacle = ObjectPooler.SharedInstance.GetPooledObject("Obstacle");
        if (obstacle)
        {
            obstacle.transform.position = transform.position;
            obstacle.transform.rotation = Quaternion.identity;
            obstacle.SetActive(true);
        }
    }
}
