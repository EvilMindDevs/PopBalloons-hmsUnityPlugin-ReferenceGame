using UnityEngine;
using TMPro;
using System.Collections.Generic;
//using HmsPlugin;
//using HuaweiMobileServices.Game;
//using HuaweiMobileServices.Utils;
using System.Linq;
using System;

public class TotalScore : MonoBehaviour
{
    TextMeshProUGUI scoreValueText;

    [SerializeField]
    private GameObject winText;

    [SerializeField]
    private GameObject loseText;

    [SerializeField] 
    private GameObject scoreCanvas;

    public int finalScoreOfAll;

    //HMSGameServiceManager GameServiceManager;

    //Achievement beginnerScorer;
    //Achievement mediumScorer; 
    //Achievement masterScorer;

    //private void Awake()
    //{
    //    HMSGameServiceManager.Instance.Init();


    //}
    //private void Start()
    //{
    //    HMSAchievementsManager.Instance.OnGetAchievementsListSuccess = OnGetAchievemenListSuccess; //serverdan sonuc success geliyo
    //    HMSAchievementsManager.Instance.OnGetAchievementsListFailure = OnGetAchievementsListFailure; //serverdan sonuc fail geliyo
    //    HMSAchievementsManager.Instance.GetAchievementsList();
    //    HMSLeaderboardManager.Instance.OnSubmitScoreSuccess = OnSubmitScoreSuccess;
    //    HMSLeaderboardManager.Instance.OnSubmitScoreFailure = OnSubmitScoreFailure;
       
    //}

    //private void OnSubmitScoreFailure(HMSException obj)
    //{
    //    Debug.Log("failure leaderboard");
    //}

    //private void OnSubmitScoreSuccess(ScoreSubmissionInfo obj)
    //{
    //    Debug.Log("success leaderboard");
    //}

    public void GetFinalScore(int thisScore)
    {
        scoreValueText = GameObject.Find("ScoreValue").GetComponent<TextMeshProUGUI>();
        finalScoreOfAll += thisScore;
        //HMSLeaderboardManager.Instance.SubmitScore(HMSLeaderboardConstants.First, 50); // Neden 50
        CheckFinalScore();
        //UnlockAchievements(finalScoreOfAll);
        scoreValueText.SetText(finalScoreOfAll.ToString());
    }

    private void CheckFinalScore()
    {
        if (finalScoreOfAll < 0)
        {
            scoreCanvas.SetActive(false);
            loseText.SetActive(true);
            GameObject.Find(Constants.gameControllerObject).GetComponent<UIController>().EndTheGame();
            //UnlockAchievements(finalScoreOfAll);

        }
        else if (finalScoreOfAll >= 50)
        {
            winText.SetActive(true);
            GameObject.Find(Constants.gameControllerObject).GetComponent<UIController>().EndTheGame();
            //UnlockAchievements(finalScoreOfAll);
        }
    }
    //private void OnGetAchievemenListSuccess(IList<Achievement> achievementList)
    //{
    //    //beginnerScorer = achievementList.First(ach => ach.Id == HMSAchievementConstants.BeginnerScore);
    //    beginnerScorer = achievementList[0];
    //    mediumScorer = achievementList[1];
    //    masterScorer = achievementList[2];
         
    //}
    //private void UnlockAchievements(int finalScore)
    //{
    //    if (finalScore <= 0)
    //    {
    //        HMSAchievementsManager.Instance.Reach(HMSAchievementConstants.BeginnerScore);
    //        //HMSAchievementsManager.Instance.UnlockAchievement(beginnerScorer.Id);
    //        //HMSAchievementsManager.Instance.UnlockAchievement(HMSAchievementConstants.BeginnerScorer); -> same as above
    //        HMSAchievementsManager.Instance.RevealAchievement(mediumScorer.Id);
    //    }
    //    else if ( beginnerScorer.State == 3 && mediumScorer.State != 3)
    //    {
    //        Debug.Log("ikinci if");
    //        HMSAchievementsManager.Instance.UnlockAchievement(mediumScorer.Id);
    //        HMSAchievementsManager.Instance.RevealAchievement(masterScorer.Id);
    //    }
    //    else if (mediumScorer.State == 3 && masterScorer.State != 3)
    //    {
    //        Debug.Log("ucuncu if");
    //        var clickToPop = GameObject.Find(Constants.gameControllerObject).GetComponent<ClickToPop>();
    //        if (clickToPop.BlackBalloonCount >= 3)
    //        {
    //            HMSAchievementsManager.Instance.UnlockAchievement(masterScorer.Id);
    //        }

    //    }
    //}
    //private void OnGetAchievementsListFailure(HMSException obj)
    //{
    //    Debug.Log("OnGetAchievementsListFailure with code: " + obj.ErrorCode);
    //}
    //public void ShowAchievements()
    //{
    //    HMSAchievementsManager.Instance.ShowAchievements();
    //}
    //public void ShowLeaderBoard()
    //{
    //    HMSLeaderboardManager.Instance.ShowLeaderboards();
    //}
}
