using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    int spawnY;
    int spawnYMin;
    int spawnYMax;

    Vector3 spawnPosition = new Vector3(0, -10, 0);

    float fakeTime;
    bool easySpawned;
    bool mediumSpawned;
    bool hardSpawned;

    GameObject obstacleToSpawn;

    [SerializeField] List<GameObject> prefabs;
    [SerializeField]List<GameObject> obstacles = new List<GameObject>();

    private void Start()
    {
        GameManager.Instance.gameOver += () => this.enabled = false;

        obstacles.Add(prefabs[0]);
        prefabs.RemoveAt(0);
        spawnYMin = 7;
        spawnYMax = 9;

        easySpawned = false;
        mediumSpawned = false;
        hardSpawned = false;
    }

    private void Update()
    {
        fakeTime = GameManager.Instance.GameSpeed - 1;

        UpdateObstacles();
        SpawnObstacle();
    }

    /// <summary>
    /// will spawn obstacles if not far from camera position
    /// </summary>
    private void SpawnObstacle()
    {
        if (Camera.main.transform.position.y - spawnPosition.y < 30)
        {
            obstacleToSpawn = obstacles[Random.Range(0, obstacles.Count)];
            GameObject go = Instantiate(obstacleToSpawn, spawnPosition, Quaternion.identity);
            go.GetComponent<Animator>().Play(0, 0, Random.Range(0f, 1f));
            spawnY = Random.Range(spawnYMin, spawnYMax);
            spawnPosition.y -= spawnY;
        }
    }

    /// <summary>
    /// If time requirements meet, obstacles list will be updated according to prefabs 
    /// </summary>
    private void UpdateObstacles()
    {
        if (fakeTime > 0.22f && !easySpawned)
        {
            obstacles.Add(prefabs[0]);
            obstacles.Add(prefabs[1]);

            prefabs.RemoveAt(1);
            prefabs.RemoveAt(0);

            spawnYMin = 9;
            spawnYMax = 11;

            easySpawned = true;
        }

        else if (fakeTime > 0.7f && !mediumSpawned)
        {
            obstacles.Add(prefabs[0]);
            obstacles.Add(prefabs[1]);
            obstacles.Add(prefabs[2]);

            prefabs.RemoveAt(2);
            prefabs.RemoveAt(1);
            prefabs.RemoveAt(0);

            spawnYMin = 10;
            spawnYMax = 15;

            mediumSpawned = true;
        }

        else if (fakeTime > 1.3f && !hardSpawned)
        {
            obstacles.Add(prefabs[0]);
            obstacles.Add(prefabs[1]);

            prefabs.RemoveAt(1);
            prefabs.RemoveAt(0);

            spawnYMin = 13;
            spawnYMax = 20;

            hardSpawned = true;
        }
    }
}
