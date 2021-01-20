using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    Button diveButton;
    TextMeshProUGUI scoreText;
    Image staminaBar;

    public float  score;
    public float stamina;

    public override void Init()
    {
        diveButton = GameObject.Find("DiveButton").GetComponent<Button>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Image>();

        diveButton.gameObject.SetActive(false);
        staminaBar.gameObject.SetActive(false);

        //scoreText.text = PlayerPrefs.GetInt(bestScore);

        GameManager.Instance.startGame += this.StartGame;
    }

    void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            scoreText.text = ((int)score).ToString();
            staminaBar.fillAmount = stamina / 100;
        }
    }

    void StartGame()
    {
        diveButton.gameObject.SetActive(true);
        staminaBar.gameObject.SetActive(true);
    }
}
