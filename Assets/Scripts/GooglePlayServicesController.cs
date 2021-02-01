using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class GooglePlayServicesController : MonoBehaviour
{
    bool success;
    static GooglePlayServicesController playerInstance;
    void Start()
    {
        DontDestroyOnLoad(this);

        if (playerInstance == null)
        {
            playerInstance = this;

            PlayGamesPlatform.Activate();
            LogIn();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static void LogIn()
    {
        Social.localUser.Authenticate(success => { });
    }

    public static void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            LogIn();
        }
    }

    public static void AddScoreToLeaderboard(int score, string leaderboard)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboard, success => { });
        }
    }
}