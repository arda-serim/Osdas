using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class GameManager : MonoSingleton<GameManager>
{
    public float GameSpeed { get; private set; } = 1;

    float score;

    public Action startGame;

    public bool gameStarted = false;

    public override void Init()
    {
        EnhancedTouchSupport.Enable();

        startGame += () => gameStarted = true;
    }

    void FixedUpdate()
    {
        if (gameStarted)
        {
            GameSpeed += 0.001f;

            score += 1 * GameSpeed;

            UIManager.Instance.score = this.score;
        }
    }

    void Update()
    {
        GameStarted();
    }

    void GameStarted()
    {
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
        {
            startGame();
        }
    }
}
