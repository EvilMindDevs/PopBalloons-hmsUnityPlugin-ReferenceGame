using HmsPlugin;
using HuaweiMobileServices.Ads;
using HuaweiMobileServices.IAP;
using HuaweiMobileServices.Id;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
    public bool IsSubscriptionActive { get; private set; }

    public GameObject rewardedAdPanel;

    private bool didRewardedEarned = false;

    void Start()
    {
        if (PlayerPrefs.GetString("isSubscriptionActive") == "false")
        {
          
            HMSAdsKitManager.Instance.OnRewardedAdLoaded = OnRewardedAdLoaded;
            HMSAdsKitManager.Instance.OnRewardAdCompleted = OnRewardAdCompleted;


            HMSAdsKitManager.Instance.ConsentOnFail = OnConsentFail;
            HMSAdsKitManager.Instance.ConsentOnSuccess = OnConsentSuccess;
            HMSAdsKitManager.Instance.RequestConsentUpdate();

           
        }
    }

    private void OnConsentSuccess(ConsentStatus arg1, bool arg2, IList<AdProvider> arg3)
    {
        Debug.Log("Consent Success");
    }

    private void OnConsentFail(string error)
    {
        Debug.LogError("Consent Failed:" + error);
    }

    public void DisableAds()
    {
        Debug.Log("subs active");
        IsSubscriptionActive = true;
        PlayerPrefs.SetString("isSubscriptionActive", "true");
        HMSAdsKitManager.Instance.HideBannerAd();
    }

    public void ShowAds()
    {
       

        HMSAdsKitManager.Instance.ShowBannerAd();
        if (HMSAdsKitManager.Instance.IsInterstitialAdLoaded)
        {
            HMSAdsKitManager.Instance.ShowInterstitialAd();
        }

        if (HMSAdsKitManager.Instance.IsRewardedAdLoaded)
        {
            rewardedAdPanel.SetActive(true);
        }
        else
        {
            HMSAdsKitManager.Instance.LoadRewardedAd();
        }

    }
    public void OnRewardedAdButton()
    {
        HMSAdsKitManager.Instance.ShowRewardedAd();
    }
    private void OnRewardedAdLoaded()
    {
        if (!didRewardedEarned) // With this, you can prevent your users from watching your rewarded ads repeatedly. Will be reload rewarded ads after watching it.
            rewardedAdPanel.SetActive(true);
    }

    private void OnRewardAdCompleted()
    {
        Debug.Log("ON REWARD COMPLETED!");
        rewardedAdPanel.SetActive(false);
        TotalScore totalScore = FindObjectOfType<TotalScore>();
        totalScore.DoubleScore();

        didRewardedEarned = true;
    }

}
