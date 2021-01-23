using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public float GameSpeed { get; private set; } = 1;

    float score;

    public Action startGame;
    public Action gameOver;

    public bool gameRunning = false;

    public override void Init()
    {
        startGame += () => gameRunning = true;
        gameOver += () =>
        {
            gameRunning = false;

            if (PlayerPrefs.GetFloat("BestScore") < score)
                PlayerPrefs.SetFloat("BestScore", score);
        };
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
}
