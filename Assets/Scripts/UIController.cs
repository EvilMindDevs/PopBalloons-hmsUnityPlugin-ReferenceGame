using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//using HuaweiMobileServices.Analystics;
//using HuaweiMobileServices.Utils;
using UnityEngine.UI;
using System.Net.Mail;
//using HuaweiMobileServices.Game;
//using HmsPlugin;
//using HuaweiMobileServices.InAppComment;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameCanvas;
   
   
    public TextMeshProUGUI blackBalloonCountValue;
    public TextMeshProUGUI blueBalloonCountValue;
    public TextMeshProUGUI greenBalloonCountValue;

    //HMSAnalyticsKitManager analyticsKitManager;
   


    private void Start()
    {
        Debug.Log("ShowInAppComment");
        //InAppComment.ShowInAppComment();
        //analyticsKitManager = HMSAnalyticsKitManager.Instance;
        gameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        
    }
    public void OnStartButton()
    {
        Time.timeScale = 1;
        gameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

   
    public void EndTheGame()
    {
        
        Time.timeScale = 0;
        var clickToPop = GameObject.Find(Constants.gameControllerObject).GetComponent<ClickToPop>();
        clickToPop.enabled = false;
        gameOverCanvas.SetActive(true);
       
        CountsToText();
      
    }

    private void CountsToText()
    {
        var clickToPop = GameObject.Find(Constants.gameControllerObject).GetComponent<ClickToPop>();

        blackBalloonCountValue.SetText(("Black Balloon Count: " + clickToPop.BlackBalloonCount));
        blueBalloonCountValue.SetText(("Blue Balloon Count: " + clickToPop.BlueBalloonCount));
        greenBalloonCountValue.SetText(("Green Balloon Count: " + clickToPop.GreenBalloonCount));

        //analyticsKitManager.SendEventWithBundle("BlackBalloonPop", "BlackBalloon" , (clickToPop.BlackBalloonCount).ToString());
        
        
       
        
    }
   
    public void OnMainMenu()
    {
        gameCanvas.SetActive(true);
        
    }
    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }

  
    

}
