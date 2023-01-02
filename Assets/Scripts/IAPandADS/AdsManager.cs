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
        Debug.Log("BBBBBBBBBBUUUUUUUUURRRRRRRRRRRRRAAAAAAAAAAAAAAAAADDDDDDDDDDAAAAAAAAAAA");
        if (PlayerPrefs.GetInt("isSubscriptionActive") == 0)
        {
            Debug.Log("simdi burada issubsactive playerprefi 0 mis yani!!!!!!!!!!!");
            HMSAdsKitManager.Instance.OnRewardedAdLoaded = OnRewardedAdLoaded;
            HMSAdsKitManager.Instance.OnRewardAdCompleted = OnRewardAdCompleted;


            HMSAdsKitManager.Instance.ConsentOnFail = OnConsentFail;
            HMSAdsKitManager.Instance.ConsentOnSuccess = OnConsentSuccess;
            HMSAdsKitManager.Instance.RequestConsentUpdate();

           
        }
        var builder = HwAds.RequestOptions.ToBuilder();

        //builder
        //    .SetConsent("tcfString")
        //    .SetNonPersonalizedAd((int)NonPersonalizedAd.ALLOW_ALL)
        //    .Build();

        //bool requestLocation = true;
        //var requestOptions = builder.SetConsent("testConsent").SetRequestLocation(requestLocation).Build();

        //Debug.Log($"RequestOptions NonPersonalizedAds:  {requestOptions.NonPersonalizedAd}");
        //Debug.Log($"Consent: {requestOptions.Consent}");
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
        PlayerPrefs.SetInt("isSubscriptionActive", 1);
        HMSAdsKitManager.Instance.HideBannerAd();
    }

    public void ShowAds()
    {
        if (PlayerPrefs.GetInt("isSubscriptionActive") == 0)
        {
            Debug.Log("simdi burada showads icinde issubsactive playerprefi 0 mis yani!!!!!!!!!!!");
            HMSAdsKitManager.Instance.ShowBannerAd();
            if (HMSAdsKitManager.Instance.IsInterstitialAdLoaded)
            {
                HMSAdsKitManager.Instance.ShowInterstitialAd();
            }

            rewardedAdPanel.SetActive(true);

            //Debug.Log("HMSAdsKitManager.Instance.IsRewardedAdLoaded ============ " + HMSAdsKitManager.Instance.IsRewardedAdLoaded);

            //while (!HMSAdsKitManager.Instance.IsRewardedAdLoaded)
            //{
            //    HMSAdsKitManager.Instance.LoadRewardedAd();
            //}
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

    public void OnApplicationFocus(bool focus)
    {
        if(focus && didRewardedEarned)
        {
            Debug.Log("app has gained focus");
            //OnRewardAdCompleted?.invoke 

        }
    }

}
