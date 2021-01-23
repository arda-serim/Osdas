using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    Button button;
    TextMeshProUGUI scoreText;
    Image staminaBar;

    public float  score;
    public float stamina;

    public override void Init()
    {
        button = GameObject.Find("Button").GetComponent<Button>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Image>();

        staminaBar.gameObject.SetActive(false);

        scoreText.text = ((int)PlayerPrefs.GetFloat("BestScore")).ToString();

        GameManager.Instance.startGame += this.StartGame;
        GameManager.Instance.gameOver += () =>
        {
            staminaBar.gameObject.SetActive(false);
            button.gameObject.SetActive(false);
        };
    }

    void Update()
    {
        if (GameManager.Instance.gameRunning)
        {
            scoreText.text = ((int)score).ToString();
            staminaBar.fillAmount = stamina / 100;
        }
    }

    void StartGame()
    {
        staminaBar.gameObject.SetActive(true);
    }
}
