using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject storeCanvas;
   
   
    public TextMeshProUGUI blackBalloonCountValue;
    public TextMeshProUGUI blueBalloonCountValue;
    public TextMeshProUGUI greenBalloonCountValue;

    public GameObject nativeAdButton;

    //HMSAnalyticsKitManager analyticsKitManager;

    StoreManager storeManager;

    


    private void Start()
    {
        Debug.Log("ShowInAppComment");
       
        //InAppComment.ShowInAppComment();
        //analyticsKitManager = HMSAnalyticsKitManager.Instance;
        gameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        storeCanvas.SetActive(false);
        
    }
    public void OnStartButton()
    {
        Time.timeScale = 1;
        gameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        nativeAdButton.SetActive(false);
    }

   
    public void EndTheGame()
    {
        nativeAdButton.SetActive(true);
        Time.timeScale = 0;
        var clickToPop = GameObject.Find(Constants.gameControllerObject).GetComponent<ClickToPop>();
        clickToPop.enabled = false;
        gameOverCanvas.SetActive(true);

        AdsManager adsManager = GameObject.FindObjectOfType<AdsManager>();
        if (adsManager != null)
        {
            adsManager.ShowAds();
        }


    }

    //private void CountsToText()
    //{
    //    var clickToPop = GameObject.Find(Constants.gameControllerObject).GetComponent<ClickToPop>();

    //    blackBalloonCountValue.SetText(("Black Balloon Count: " + clickToPop.BlackBalloonCount));
    //    blueBalloonCountValue.SetText(("Blue Balloon Count: " + clickToPop.BlueBalloonCount));
    //    greenBalloonCountValue.SetText(("Green Balloon Count: " + clickToPop.GreenBalloonCount));

    //    //analyticsKitManager.SendEventWithBundle("BlackBalloonPop", "BlackBalloon" , (clickToPop.BlackBalloonCount).ToString());
        
        
        
    //}
   
    public void OnMainMenu()
    {
        gameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        storeCanvas.SetActive(false);

    }
    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }

    public void OnStoreButton()
    {
        storeCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);

        AfterStoreButton();

    }

    private void AfterStoreButton()
    {
        Debug.Log("Trying to find store manager object");
        StoreManager storeManager = GameObject.FindObjectOfType<StoreManager>();

        if (storeManager == null)
            Debug.Log("store manager is null!!!!!!!!!!");
        else
        {
            storeManager.FillProducts();
        }
    }
  
    

}
