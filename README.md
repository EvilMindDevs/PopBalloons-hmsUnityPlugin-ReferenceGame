# PopBalloons-hmsUnityPlugin-ReferenceGame

Bloon Popping
Welcome to Bloon Popping, a fun and challenging game that tests your balloon popping skills! The goal of the game is to pop as many balloons as possible before they reach the top of the screen. Each time you pop a blue, green, or pink balloon, you will earn points. But be careful, if you pop a black balloon, you will lose.

The game also features in-app purchases and ads, allowing you to enhance your experience and support the development of the game.

The game uses the HMS in-app purchase kit and Ads kit to show a demo of it to how to use.

Please enjoy and have fun playing Balloon Popping!

In this project, we explain the use of [Unity Plugin](https://github.com/EvilMindDevs/hms-unity-plugin) of Huawei Mobile Services on this game.

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

`Purpose In Project` :  Monetizing with Interstial Ads and Banner Ads.


`Use In Project` : 
 
 1. Banner Ads : By calling this method `ShowBanner()` at AdsManager.cs 102.We are calling this method at first,and you can hide banner ads by calling `HideBanner()` at AdsManager.cs 112.

   ```csharp
    #region Ads: Banner

    public void ShowBanner()
    {
        if (!IsAdsActive)
            return;

        GLog.Log($"ShowBanner", GLogName.AdsManager);

        HMSAdsKitManager.Instance.ShowBannerAd();
    }

    public void HideBanner()
    {
        GLog.Log($"HideBanner", GLogName.AdsManager);

        HMSAdsKitManager.Instance.HideBannerAd();
    }

    #endregion
  ```


2. Interstial Ads : By calling this method `ShowInterstitial()` at AdsManager.cs 145.We are calling this method at the end of sessions.

```csharp
    #region Ads: Interstitial

    public void ShowInterstitial()
    {
        if (!IsAdsActive)
            return;

        GLog.Log($"ShowInterstitial", GLogName.AdsManager);

        HMSAdsKitManager.Instance.ShowInterstitialAd();
    }

    #endregion
```





## In App Comment

`Purpose In Project` : Providing users to submit ratings and make comments for app without leaving the application.

 `Use In Project` : Automatically running at the session end callback  `OnSessionEnd` at GameManager.cs 245.

```csharp
    #region Events: OnSessionEnd

    private void OnSessionEnd(object sender, GEvent<object> eventData)
    {
        GLog.Log($"OnSessionEnd", GLogName.GameManager);
        InAppComment.ShowInAppComment();
    }

    #endregion
```

