using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem.OnScreen;

public class UIManager : MonoSingleton<UIManager>
{
    Button startGameButton;
    Button restartGameButton;
    TextMeshProUGUI scoreText;
    Image staminaBar;

    public float  score;
    public float stamina;

    public override void Init()
    {
        startGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
        restartGameButton = GameObject.Find("RestartGameButton").GetComponent<Button>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Image>();

        staminaBar.gameObject.SetActive(false);

        scoreText.text = ((int)PlayerPrefs.GetFloat("BestScore")).ToString();

        startGameButton.gameObject.SetActive(true);
        restartGameButton.gameObject.SetActive(false);

        GameManager.Instance.startGame += asd;
        GameManager.Instance.gameOver += dsa;
    }

    void Update()
    {
        if (GameManager.Instance.gameRunning)
        {
            scoreText.text = ((int)score).ToString();
            staminaBar.fillAmount = stamina / 100;
        }
    }

    void asd()
    {
        staminaBar.gameObject.SetActive(true);

        startGameButton.gameObject.SetActive(false);
        restartGameButton.gameObject.SetActive(false);
    }

    void dsa()
    {
        staminaBar.gameObject.SetActive(false);

        scoreText.gameObject.GetComponent<Animation>().Play();

        startGameButton.gameObject.SetActive(false);
        restartGameButton.gameObject.SetActive(true);
    }
}
