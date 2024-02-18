using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidsModels;

    [SerializeField] private int amountAsteroidsSpawn = 10;

    private float minRandomSpawn = -500;
    [SerializeField] private float maxRandomSpawn = 500;

    [SerializeField] private Transform asteroids;

    private void Start()
    {
        SpawnAsteroid();
    }

    void SpawnAsteroid()
    {
        for (int i = 0; i < amountAsteroidsSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minRandomSpawn, maxRandomSpawn), Random.Range(minRandomSpawn, maxRandomSpawn), Random.Range(minRandomSpawn, maxRandomSpawn));
            GameObject asteroidInstance = Instantiate(asteroidsModels[Random.Range(0, asteroidsModels.Length)], randomPosition, Quaternion.identity, asteroids);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(maxRandomSpawn * 2, maxRandomSpawn * 2, maxRandomSpawn *2));
    }
}
