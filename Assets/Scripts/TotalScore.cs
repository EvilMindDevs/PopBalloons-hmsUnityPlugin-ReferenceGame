using UnityEngine;
using TMPro;

public class TotalScore : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private GameObject scoreCanvas;
    private TextMeshProUGUI scoreValueText;

    [Header("Game Constants")]
    //private const int WIN_SCORE = 200;
    private const int LOSE_SCORE = 0;

    private int finalScoreOfAll;

    public GameObject rewardAdsPanel;

    private bool didRewardedEarned = false;

    private void Start()
    {
        scoreValueText = GameObject.Find("ScoreValue").GetComponent<TextMeshProUGUI>();
    }

    public void GetFinalScore(int thisScore)
    {
        finalScoreOfAll += thisScore;
        scoreValueText.SetText(finalScoreOfAll.ToString());

        if (finalScoreOfAll < LOSE_SCORE)
        {
            scoreCanvas.SetActive(false);
            loseText.SetActive(true);
            UIController uiController = FindObjectOfType<UIController>();
            uiController.EndTheGame();
        }
        //else
        //{
        //    winText.SetActive(true);
        //    UIController uiController = FindObjectOfType<UIController>();
        //    uiController.EndTheGame();
        //}
    }

    public void OnRewardedAdButtonClick()
    {
        //HMSAdsKitManager.Instance.ShowRewardedAd();
        DoubleScore(finalScoreOfAll);
    }

    public void DoubleScore(int finalScore)
    {
        finalScore *= 2;
        scoreValueText.SetText(finalScore.ToString());
    }
}
