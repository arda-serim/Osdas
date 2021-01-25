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

    AudioListener audioListener;

    public override void Init()
    {
        GameObject.Find("Global Volume").GetComponent<Volume>().profile.TryGet(out depthOfField);

        startGame += () => gameRunning = true;
        gameOver += () =>
        {
            if (PlayerPrefs.GetInt("BestScore") < score)
                PlayerPrefs.SetInt("BestScore", (int)score);

            GooglePlayServicesController.AddScoreToLeaderboard(PlayerPrefs.GetInt("BestScore"), GPGSIds.leaderboard_best_score);

            depthOfField.mode.overrideState = true;

            gameRunning = false;
        };

        RenderSettings.skybox = skyboxes[UnityEngine.Random.Range(0, skyboxes.Count)];

        audioListener = Camera.main.GetComponent<AudioListener>();
        audioListener.enabled = PlayerPrefs.GetInt("AudioListener", 1) == 1 ? true : false;
    }

    void FixedUpdate()
    {
        if (gameRunning)
        {
            GameSpeed += 0.0007f;

            score += 1 * GameSpeed;

            UIManager.Instance.score = (int)this.score;
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

    public void SoundOnOff()
    {
        audioListener.enabled = !audioListener.enabled;
        PlayerPrefs.SetInt("AudioListener", audioListener.enabled ? 1 : 0);
        UIManager.Instance.ControlSoundButtons();
    }
}
