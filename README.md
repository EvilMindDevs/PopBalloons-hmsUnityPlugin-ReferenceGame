# PopBalloons-hmsUnityPlugin-ReferenceGame

Bloon Popping
Welcome to Bloon Popping, a fun and challenging game that tests your balloon popping skills! The goal of the game is to pop as many balloons as possible before they reach the top of the screen. Each time you pop a blue, green, or pink balloon, you will earn points. But be careful, if you pop a black balloon, you will lose.

![1](https://user-images.githubusercontent.com/41302444/212878089-bb61f780-4064-40b1-bbc9-009ebbf8e39e.png)


The game also features in-app purchases and ads, allowing you to enhance your experience and support the development of the game.

The game uses the HMS in-app purchase kit and Ads kit to show a demo of it to how to use.

Please enjoy and have fun playing Balloon Popping!

In this project, we explain the use of [Unity Plugin](https://github.com/EvilMindDevs/hms-unity-plugin) of Huawei Mobile Services on this game.

![Screenshot 2023-01-17 134044](https://user-images.githubusercontent.com/41302444/212878219-ffd3a069-7b9d-473b-8a46-9d2073947c64.png)


!Important issue. After logged in wait couple seconds before clicking store button to get products properly. They might not be fully taken from server.(Their name and prices might not match or There could be missing product.)

# Huawei Mobile Services Plugin

The HMS Unity plugin helps you integrate all the power of Huawei Mobile Services in your Unity game.

[Requirements](https://evilminddevs.gitbook.io/hms-unity-plugin-beta/getting-started/what-you-will-need)

[Quick Start](https://evilminddevs.gitbook.io/hms-unity-plugin-beta/getting-started/quick-start)



## Account Kit

`Purpose In Project` :  Sign in automatically.


 `Use In Project` : By calling this method `HMSAccountKitManager.Instance.SignIn();` at AccountManager.cs 45

   ```csharp

    

    void Start()
    {
        HMSAccountKitManager.Instance.OnSignInSuccess = OnLoginSuccess;
        HMSAccountKitManager.Instance.OnSignInFailed = OnLoginFailure;

        AccountKitLog?.Invoke(NOT_LOGGED_IN);

    }


  ```



## IAP

`Purpose In Project` :  Selling the product of "Remove Ads".


 `Use In Project` : 
 
 1. By clicking "No Ads" button in game menu.This button calls this method has on clicker at StoreManager.cs 204

   ```csharp

    #region ButtonClick: BuyProduct

    product.transform.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(delegate { BuyProduct(productInfo.ProductId); });
    
    #endregion

  ```

2. ` BuyProduct(productInfo.ProductId); ` command invokes `OnBuyProduct(string productID)` at StoreManager.cs 33


```csharp


    #region Events: OnBuyProduct

    private void OnBuyProduct(string productID)
    {
        HMSIAPManager.Instance.BuyProduct(productID);
    }

    #endregion


```

  3. If purchase request return success for all products callback method will be called automatically.In this method we handle the having "No Ads", "Booster" and "Pink Color" item.


   ```csharp

    #region Events: BuyProductSuccess

     private void OnBuyProductSuccess(PurchaseResultInfo obj)
    {
        Debug.Log("OnBuyProductSuccess:" + obj.InAppPurchaseData.ProductName);

        if (obj.InAppPurchaseData.ProductId == HMSIAPConstants.NoAds)
        {
            AdsManager adsManager = gameObject.AddComponent<AdsManager>();
            adsManager.DisableAds();
            Debug.Log("NOADS HAS PURCHASED");
            
            
        }
        else if (obj.InAppPurchaseData.ProductId == HMSIAPConstants.Booster)
        {
            int currentBoosterCount = PlayerPrefs.GetInt("booster_count", 0);
            currentBoosterCount++;
            PlayerPrefs.SetInt("booster_count", currentBoosterCount);
            PlayerPrefs.Save();
            Debug.Log("BOOSTER HAS PURCHASED");

            UIController uIController = FindObjectOfType<UIController>();
            uIController.DisplayBoosterCount();

        }
        else if (obj.InAppPurchaseData.ProductId == HMSIAPConstants.PinkColor)
        {
            BalloonColor balloonColor = new BalloonColor();
            balloonColor.UnlockColor();
            Debug.Log("PINK COLOR HAS PURCHASED");
        }
       
    }

    #endregion


  ```




## Ads Kit

`Purpose In Project` :  Monetizing with Interstial Ads, Banner Ads, Native Ads and Splash Ads.


`Use In Project` : 
 
 1. Banner Ads and Interstitial Ads : By calling this method `ShowBanner()` at AdsManager.cs 69 and HMSAdsKitManager.Instance.ShowInterstitialAd(); at AdsManager.cs 72.We are calling these methods at first,a nd you can hide banner ads by calling `HideBanner()` at AdsManager.cs 61.

   ```csharp
     public void ShowAds()
    {
        
            HMSAdsKitManager.Instance.ShowBannerAd();
            if (HMSAdsKitManager.Instance.IsInterstitialAdLoaded)
            {
                HMSAdsKitManager.Instance.ShowInterstitialAd();
            }

            rewardedAdPanel.SetActive(true);

       
        
    }
  ```


2. Rewarded Ads : By calling this method invoking events using HMSAdsKitManager.Instance.OnRewardedAdLoaded at AdsManager.cs 35 and 36  

```csharp
  
  HMSAdsKitManager.Instance.OnRewardedAdLoaded = OnRewardedAdLoaded;
  HMSAdsKitManager.Instance.OnRewardAdCompleted = OnRewardAdCompleted;

```
We give awards when the ads completed on  OnRewardAdCompleted() method at AdsManager.cs 96.

```csharp

    private void OnRewardAdCompleted()
    {
        Debug.Log("ON REWARD COMPLETED!");
        rewardedAdPanel.SetActive(false);
        TotalScore totalScore = FindObjectOfType<TotalScore>();
        totalScore.DoubleScore();

        didRewardedEarned = true;
    }

```



## In App Comment

`Purpose In Project` : Providing users to submit ratings and make comments for app without leaving the application.

 `Use In Project` : Its called in Start() function at UIController.cs 14.

```csharp
    

   private void Start()
    {
        Debug.Log("ShowInAppComment");
        InAppComment.ShowInAppComment();
        
        gameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        storeCanvas.SetActive(false);

        
    }

```

