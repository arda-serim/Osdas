using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class GooglePlayServicesController : MonoBehaviour
{
    bool success;
    void Start()
    {
        PlayGamesPlatform.Activate();
        LogIn();
    }
    public static void LogIn()
    {
        Social.localUser.Authenticate(success => { });
    }

    public static void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public static void AddScoreToLeaderboard(int score, string leaderboard)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboard, success => { });
        }
    }
}