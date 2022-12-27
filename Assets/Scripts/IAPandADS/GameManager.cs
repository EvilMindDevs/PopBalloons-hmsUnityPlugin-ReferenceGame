using System.Collections.Generic;
using UnityEngine;
using HmsPlugin;
using UnityEngine.UI;
using HuaweiMobileServices.Ads;

public class AdsManager : MonoBehaviour
{
    private AdsDisabler adsDisabler;

   

    private float adInterval = 5.0f;
    private float adDuration = 2.0f;

    public GameObject rewardAdsPanel;

    private bool didRewardedEarned = false;
    public bool boosterPurchased = false;

  

    void Start()
    {

        adsDisabler = GameObject.Find("GameController").GetComponent<AdsDisabler>();
        if(adsDisabler.IsSubscriptionActive == false)
        {
            HMSAdsKitManager.Instance.OnRewardAdCompleted = OnRewardAdCompleted;
            HMSAdsKitManager.Instance.OnRewardedAdLoaded = OnRewardedAdLoaded;

            Debug.Log("[HMS] ShowSplashVideo!");
            HMSAdsKitManager.Instance.LoadSplashAd("testd7c5cewoj6", SplashAd.SplashAdOrientation.LANDSCAPE);

            //Debug.Log("[HMS] ShowSplashVideo!");
            //HMSAdsKitManager.Instance.LoadSplashAd("testd7c5cewoj6", SplashAd.SplashAdOrientation.LANDSCAPE);

        }



    }

    void Update()
    {
        if (adsDisabler.IsSubscriptionActive == false)
        {
            HMSAdsKitManager.Instance.ShowBannerAd();
            Debug.Log("!!!!!banner adloaded!!!!");
         
        }
        HMSAdsKitManager.Instance.HideBannerAd();

    }

    private void OnRewardedAdLoaded()
    {
        if (!didRewardedEarned) 
            rewardAdsPanel.gameObject.SetActive(true);


    }

    private void OnRewardAdCompleted()
    {
        rewardAdsPanel.gameObject.SetActive(false);
        didRewardedEarned = true;
    }

    public void EndGameAds()
    {
        if (adsDisabler.IsSubscriptionActive == false)
        {
            if (HMSAdsKitManager.Instance.IsInterstitialAdLoaded)
            {
                HMSAdsKitManager.Instance.ShowInterstitialAd();
            }

        }
    }
    public void OnBoosterPurchased()
    {
        boosterPurchased = true;
    }

}