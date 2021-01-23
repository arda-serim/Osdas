using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    Vector3 spawnPosition = new Vector3(0, -10, 0);

    float fakeTime;
    bool easySpawned;
    bool mediumSpawned;
    bool hardSpawned;

    [SerializeField] List<GameObject> prefabs;
    [SerializeField]List<GameObject> obstacles = new List<GameObject>();

    private void Start()
    {
        GameManager.Instance.gameOver += () => this.enabled = false;

        obstacles.Add(prefabs[0]);
        prefabs.RemoveAt(0);

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
            Instantiate(obstacles[Random.Range(0, obstacles.Count)], spawnPosition, Quaternion.identity);
            int spawnY = Random.Range(5, 10);
            spawnPosition.y -= spawnY;
        }
    }

    /// <summary>
    /// If time requirements meet, obstacles list will be updated according to prefabs 
    /// </summary>
    private void UpdateObstacles()
    {
        if (fakeTime > 0.5f && !easySpawned)
        {
            obstacles.Add(prefabs[0]);
            obstacles.Add(prefabs[1]);

            prefabs.RemoveAt(1);
            prefabs.RemoveAt(0);

            easySpawned = true;
        }

        else if (fakeTime > 1.5f && !mediumSpawned)
        {
            obstacles.Add(prefabs[0]);
            obstacles.Add(prefabs[1]);
            obstacles.Add(prefabs[2]);

            prefabs.RemoveAt(2);
            prefabs.RemoveAt(1);
            prefabs.RemoveAt(0);

            mediumSpawned = true;
        }

        else if (fakeTime > 3.0 && !hardSpawned)
        {
            obstacles.Add(prefabs[0]);
            obstacles.Add(prefabs[1]);

            prefabs.RemoveAt(1);
            prefabs.RemoveAt(0);

            hardSpawned = true;
        }
    }
}
