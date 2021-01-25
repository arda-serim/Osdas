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
    TextMeshProUGUI bestScore;
    Image staminaBar;
    Button soundButton;
    Image soundOn;
    Image soundOff;

    AudioListener audioListener;

    public int score;
    public float stamina;

    public override void Init()
    {
        startGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
        restartGameButton = GameObject.Find("RestartGameButton").GetComponent<Button>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        bestScore = GameObject.Find("BestScore").GetComponent<TextMeshProUGUI>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Image>();
        soundButton = GameObject.Find("SoundButton").GetComponent<Button>();
        soundOn = GameObject.Find("SoundOn").GetComponent<Image>();
        soundOff = GameObject.Find("SoundOff").GetComponent<Image>();
        audioListener = Camera.main.gameObject.GetComponent<AudioListener>();

        staminaBar.gameObject.SetActive(false);

        scoreText.text = PlayerPrefs.GetInt("BestScore").ToString();

        startGameButton.gameObject.SetActive(true);
        restartGameButton.gameObject.SetActive(false);

        GameManager.Instance.startGame += () =>
        {
            staminaBar.gameObject.SetActive(true);

            scoreText.gameObject.GetComponent<Animation>().Play("ScoreStartGame");

            startGameButton.gameObject.SetActive(false);
            restartGameButton.gameObject.SetActive(false);
            soundButton.gameObject.SetActive(false);
        };
        GameManager.Instance.gameOver += () =>
        {
            staminaBar.gameObject.SetActive(false);

            scoreText.text = score.ToString();
            bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
            scoreText.gameObject.GetComponent<Animation>().Play("ScoreGameOver");

            startGameButton.gameObject.SetActive(false);
            restartGameButton.gameObject.SetActive(true);
        };

        ControlSoundButtons();
    }

    void Update()
    {
        if (GameManager.Instance.gameRunning)
        {
            scoreText.text = score.ToString();
            staminaBar.fillAmount = stamina / 100;
        }
    }

    public void ControlSoundButtons()
    {
        if (audioListener.enabled)
        {
            soundOn.enabled = true;
            soundOff.enabled = false;
        }
        else
        {
            soundOn.enabled = false;
            soundOff.enabled = true;
        }
    }
}
