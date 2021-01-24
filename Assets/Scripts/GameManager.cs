using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoSingleton<GameManager>
{
    public float GameSpeed { get; private set; } = 1;

    float score;

    public Action startGame;
    public Action gameOver;

    public bool gameRunning = false;

    DepthOfField depthOfField;

    [SerializeField]List<Material> skyboxes;

    public override void Init()
    {
        GameObject.Find("Global Volume").GetComponent<Volume>().profile.TryGet(out depthOfField);

        startGame += () => gameRunning = true;
        gameOver += () =>
        {
            gameRunning = false;

            if (PlayerPrefs.GetFloat("BestScore") < score)
                PlayerPrefs.SetFloat("BestScore", score);

            depthOfField.mode.overrideState = true;
        };

        RenderSettings.skybox = skyboxes[UnityEngine.Random.Range(0, skyboxes.Count)];
    }

    void FixedUpdate()
    {
        if (gameRunning)
        {
            GameSpeed += 0.001f;

            score += 1 * GameSpeed;

            UIManager.Instance.score = this.score;
        }
    }

    public void StartGame()
    {
        startGame();
    }    
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
