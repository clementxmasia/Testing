using System.Collections;
using UnityEngine;
using Photon.Pun;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public float objectLifetime = 10f;

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
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points defined.");
            return;
        }

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        if (objectToSpawn == null)
        {
            Debug.LogWarning("No object to spawn defined.");
            return;
        }

        GameObject newObject = Instantiate(objectToSpawn, randomSpawnPoint.position, randomSpawnPoint.rotation);

        StartCoroutine(DestroyObjectAfterLifetime(newObject));
    }

    private IEnumerator DestroyObjectAfterLifetime(GameObject obj)
    {
        yield return new WaitForSeconds(objectLifetime);

        if (obj != null)
        {
            Destroy(obj);
        }
    }
}
