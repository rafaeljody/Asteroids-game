using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    public float spawnRate = 2.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f;

    public float trajectoryVariance = 15.0f;

    private void Start()
    {
        // invoke a method (spawn) every x amount of seconds (spawnRate)
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for(int i = 0; i < this.spawnAmount; i++)
        {
            // picking random point on the edge of the circle, and then multiply that by some distance away
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            // make it relative to our spawn position
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            // rotation
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // spawn it
            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection); // the negate make the asteroid going inside the spawner
        }
    }
}
