using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    static Vector3 spawnPosition = new Vector3(0, -10, 0);
    static float fakeTime = GameManager.Instance.GameSpeed;

    [SerializeField] static List<GameObject> prefabs;
    static List<GameObject> obstacles;

    private void Start()
    {
        obstacles.Add(prefabs[0]);
        prefabs.RemoveAt(0);
    }

    private void Update()
    {
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
            int spawnY = Random.Range(5, 15);
            spawnPosition.y -= spawnY;
        }
    }

    /// <summary>
    /// If time requirements meet, obstacles list will be updated according to prefabs 
    /// </summary>
    private void UpdateObstacles()
    {
        if (fakeTime > 0.5f && fakeTime < 1.5f)
        {
            obstacles.Add(prefabs[0]);
            obstacles.Add(prefabs[1]);

            prefabs.RemoveAt(1);
            prefabs.RemoveAt(0);
        }

        else if (fakeTime > 1.5f && fakeTime < 3.0f)
        {
            obstacles.Add(prefabs[0]);
            obstacles.Add(prefabs[1]);
            obstacles.Add(prefabs[2]);

            prefabs.RemoveAt(2);
            prefabs.RemoveAt(1);
            prefabs.RemoveAt(0);
        }

        else if (fakeTime > 3.0)
        {
            obstacles.Add(prefabs[0]);
            obstacles.Add(prefabs[1]);

            prefabs.RemoveAt(1);
            prefabs.RemoveAt(0);
        }
    }
}
