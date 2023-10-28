using System.Collections;
using UnityEngine;
using Photon.Pun;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;
    public float spawnInterval = 5f;

    private float lastSpawnTime;

    private void Start()
    {
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            SpawnObject();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnObject()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("No spawn point defined.");
            return;
        }

        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
