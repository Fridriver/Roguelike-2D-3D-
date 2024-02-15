using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroids;

    private int amountAsteroidsSpawn = 10;

    private float minRandomSpawn = -500;
    private float maxRandomSpawn = 500;

    private void Start()
    {
        SpawnAsteroid();
    }

    void SpawnAsteroid()
    {
        for (int i = 0; i < amountAsteroidsSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minRandomSpawn, maxRandomSpawn), Random.Range(minRandomSpawn, maxRandomSpawn), Random.Range(minRandomSpawn, maxRandomSpawn));
            Instantiate(asteroids[Random.Range(0, asteroids.Length)], randomPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(maxRandomSpawn * 2, maxRandomSpawn * 2, maxRandomSpawn *2));
    }
}
