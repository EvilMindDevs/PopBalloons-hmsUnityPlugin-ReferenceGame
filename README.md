# PopBalloons-hmsUnityPlugin-ReferenceGame

Bloon Popping
Welcome to Bloon Popping, a fun and challenging game that tests your balloon popping skills! The goal of the game is to pop as many balloons as possible before they reach the top of the screen. Each time you pop a blue, green, or pink balloon, you will earn points. But be careful, if you pop a black balloon, you will lose.

The game also features in-app purchases and ads, allowing you to enhance your experience and support the development of the game.

The game uses the HMS in-app purchase kit and Ads kit to show a demo of it to how to use.

Please enjoy and have fun playing Bloon Popping!

Huawei Mobile Services Plugin

The HMS Unity plugin helps you integrate all the power of Huawei Mobile Services in your Unity game:

# Huawei Account Kit
In App purchases: Consumable, non consumables and Subscriptions.
Ads: Interstitial, rewarded videos and banner
Push notifications
Game leaderboards and achievements
Huawei Anayltics kit
Crash Service
Remote Config
Auth Service
Drive Kit
Nearby Service
App Messaging

# Requirements
Android SDK min 21 Net 4.x

Connect your game Huawei Mobile Services in 5 easy steps
Register your app at Huawei Developer

# Import the Plugin to your Unity project
Connect your game with the HMS Kit Managers
1 - Register your app at Huawei Developer
1.1- Register at Huawei Developer
1.2 - Create an app in AppGallery Connect.
During this step, you will create an app in AppGallery Connect (AGC) of HUAWEI Developer. When creating the app, you will need to enter the app name, app category, default language, and signing certificate fingerprint. After the app has been created, you will be able to obtain the basic configurations for the app, for example, the app ID and the CPID.

Sign in to Huawei Developer and click Console.
Click the under Ecosystem services, click on App Services.
Click on the AppGallery Connect under Distribution and Promotion.
Click My apps.
On the displayed My apps page, click New app on top right.
Enter the App name, select App category (Game), and select Default language as needed.
Upon successful app creation, the App information page will automatically display. There you can find the App ID that is assigned by the system to your app.
1.3 Add Package Name
Set the package name of the created application on the AGC.

In app information page, there is a label at top saying "My Apps". Mouse hover on it and select My Project. This will lead you to the project information of your application
You should see a pop up asking about your package name for the application. Select Manually enter a package name
Fill in the application package name in the input box and click save.
Your package name should end in .huawei in order to release in App Gallery

Generate a keystore.
Create a keystore using Unity or Android Tools. make sure your Unity project uses this keystore under the Build Settings>PlayerSettings>Publishing settings

Generate a signing certificate fingerprint.
During this step, you will need to export the SHA-256 fingerprint by using keytool provided by the JDK and signature file.

Open the command window or terminal and access the bin directory where the JDK is installed.

Run the keytool command in the bin directory to view the signature file and run the command.

keytool -list -v -keystore D:\Android\WorkSpcae\HmsDemo\app\HmsDemo.jks

Enter the password of the signature file keystore in the information area. The password is the password used to generate the signature file.

Obtain the SHA-256 fingerprint from the result. Save for next step.

Add fingerprint certificate to AppGallery Connect
During this step, you will configure the generated SHA-256 fingerprint in AppGallery Connect.

In AppGallery Connect, go to My Project and select your project.
Go to the App information section, click on + button and enter the SHA-256 fingerprint that you generated earlier.
Click √ to save the fingerprint.
2 - Import the plugin to your Unity Project
To import the plugin:

Download the .unitypackage
Open your game in Unity
Choose Assets> Import Package> Custom Import Package
In the file explorer select the downloaded HMS Unity plugin. The Import Unity Package dialog box will appear, with all the items in the package pre-checked, ready to install. image
Select Import and Unity will deploy the Unity plugin into your Assets Folder
3 - Update your agconnect-services.json file.
In order for the plugin to work, some kits are in need of agconnect-json file. Please download your latest config file from AGC and import into Assets/StreamingAssets folder. image

4 - Connect your game with any HMS Kit
In order for the plugin to work, you need to select the needed kits Huawei > Kit Settings. image

It will automaticly create the GameObject for you and it has DontDestroyOnLoad implemented so you don't need to worry about reference being lost.

Now you need your game to call the Kit Managers from your game. See below for further instructions.

Account Kit (Sign In)
Call login method in order to open the login dialog. Be sure to have AccountKit enabled in Huawei > Kit Settings.

HMSAccountManager.Instance.SignIn();
Analytics kit
Enable Analtics kit from AGC
Update ...Assets\StreamingAssets\agconnect-services.json file
Send analytics function:

HMSAnalyticsManager.Instance.SendEventWithBundle(eventId, key, value);
In App Purchases
Register your products via custom editor under Huawei > Kit Settings > IAP tab. image Write your product identifier that is in AGC and select product type.

If you check "Initialize On Start" checkbox, it'll automaticly retrieve registered products on Start. If you want to initialize the IAP by yourself, call the function mentioned in below. You can also set callbacks as well.

HMSIAPManager.Instance.CheckIapAvailability();

HMSIAPManager.Instance.OnCheckIapAvailabilitySuccess += OnCheckIapAvailabilitySuccess;
HMSIAPManager.Instance.OnCheckIapAvailabilityFailure += OnCheckIapAvailabilityFailure;

private void OnCheckIapAvailabilityFailure(HMSException ex)
    {
        
    }

    private void OnCheckIapAvailabilitySuccess()
    {
        
    }
Open the Purchase dialog by calling to BuyProduct method. You can set callbacks and check which product was purchased.

HMSIAPManager.Instance.BuyProduct(string productID)

HMSIAPManager.Instance.OnBuyProductSuccess += OnBuyProductSuccess;

private void OnBuyProductSuccess(PurchaseResultInfo result)
    {
        if (result.InAppPurchaseData.ProductId == "removeAds")
        {
            // Write your remove ads logic here.
        }
    }
Restore purchases that have been bought by user before.

 HMSIAPManager.Instance.RestorePurchases((restoredProducts) =>
        {
            //restoredProducts contains all products that has been restored.
        });
You can also use "Create Constant Classes" button to create a class called HMSIAPConstants which will contain all products as constants and you can call it from your code. Such as;

HMSIAPManager.Instance.BuyProduct(HMSIAPConstants.testProduct);
Ads kit
There is a custom editor in Huawei > Kit Settings > Ads tab. image

You can enable/disable ads that you want in your project. Insert your Ad Id into these textboxes in the editor. If you want to use test ads, you can check UseTestAds checkbox that'll overwrite all ad ids with test ads.

If you want to know more details about Splash Ad and its configuration, please check this article written by @sametguzeldev here.

Then you can call certain functions such as

    HMSAdsKitManager.Instance.ShowBannerAd();
    HMSAdsKitManager.Instance.HideBannerAd();
    HMSAdsKitManager.Instance.ShowInterstitialAd();
    
    HMSAdsKitManager.Instance.OnRewarded = OnRewarded;
    HMSAdsKitManager.Instance.ShowRewardedAd();
    
    public void OnRewarded(Reward reward)
    {
       
    }
Game kit
There is a custom editor in Huawei > Kit Settings > Game Service tab. image

Check "Initialize on Start" checkbox to initialize the Game Service Kit on Start or call HMSGameManager.Instance.Init() in your custom logic.

   HMSGameManager.Instance.Init();
You can use "Create Constant Classes" button to create a class called HMSLeaderboardConstants or HMSAchievementConstants which will contain all achievements and leaderboards as constants and you can call it from your code. Such as;

    HMSLeaderboardManager.Instance.SubmitScore(HMSLeaderboardConstants.topleaderboard,50);
    HMSAchievementsManager.Instance.RevealAchievement(HMSAchievementConstants.firstshot);
You can call native calls to list achievements or leaderboards.

  HMSAchievementsManager.Instance.ShowAchievements();
  HMSLeaderboardManager.Instance.ShowLeaderboards();
App Update in Game Service
There is a method in Game Service called CheckAppUpdate that will trigger the update mechanism of HMS to detect if there is newer version in AppGallery. It triggers OnAppUpdateInfo inside HMSGameManager that is returning status,rtnCode,rtnMessage,isExit,buttonStatus. This callback gets called after CheckAppUpdate is done. If you want to receive this callback, please subscribe into it before calling CheckAppUpdate.

It requires two booleans; showAppUpdate: Making this true will prompt a native UI that will show the user there is a newer version if there is an update. forceAppUpdate: Making this true will remove the cancel button from the UI and forcing user to update.

    HMSGameManager.Instance.OnAppUpdateInfo = OnAppUpdateInfo;
    HMSGameManager.Instance.CheckAppUpdate(showAppUpdate,forceAppUpdate);
Connect API
2.1.0 version comes with Connect API features! Right now we've implemented Publishing API and PMS API. To be able to use these APIs, you need to create an API Client through AppGallery Connect.

After selecting your project on AGC, please go to Users and Permissions section. Find API key section on the left side and click Connect API. On the right side, you will see a button called "Create". Click on it to create an API Client for Connect API. image

After creating your key, please copy Client ID and Key section. image

Paste your Client ID to Client ID section, Key to Client Secret section in Token Obtainer Editor. image

Publishing API
This API here to help you to publish your apk or aab after a successfull build. You can access this API by going

Huawei>Connect API>Publishing API>Querying App Information

From Querying App Information, you can check you app name, app category and your release state. But most fun part starts after those information. Cause those informations there just for letting you know "I can communicate with AppGallery".

After informations there are a checkbox called "Upload After Build". If you select this checkbox, than Plugin will ask you everytime you do a successfull build "Should I send this apk/aab to AppGallery Connect?". If you select yes, than sending work will be started and you can check it from console or from progress bar. After uploading, you can check your apk/abb from the App Gallery Connect.

Note: If you are using AAB, you should consider reading the warning after enabling the checkbox. "Please Check the App Signing Feature Enabled on AppGallery Connect For Uploading AAB Packages"

readmePhotoCensored

PMS API
This API here to help you to manage your products. You can access this API by going

Huawei>Connect API>PMS API

Query IAP Products, You can view all of your products with or without filtering by Product ID and Product name.

You can create a product with Create a Product or import your products with Create Products.

Note: You can not edit your deleted products. Note: You can not change your products' purchase type which you created.

readmePhotoCensored

Kits Specification
Find below the specific information on the included functionalities in this plugin

Account
In App Purchases
Ads
Push notifications
Game
Analytics
Remote Config
Crash
Cloud DB
Auth Service
Drive Kit
Nearby Service
App Messaging
Account
Official Documentation on Account Kit: Documentation

In App Purchases
Official Documentation on IAP Kit: Documentation

Ads
Official Documentation on Ads Kit: Documentation

Push
Official Documentation on Push Kit: Documentation

Game
Official Documentation on Game Kit: Documentation

Analytics
Official Documentation on Analytics Kit: Documentation

Remote Config
Official Documentation on Remote Config: Documentation

Crash
Official Documentation on Crash Kit: Documentation

Cloud DB
Official Documentation on Cloud DB: Documentation

Auth Service
Official Documentation on Auth Service: Documentation

Drive Kit
Official Documentation on Drive Kit: Documentation

Nearby Service
Official Documentation on Nearby Service: Documentation

App Messaging
Official Documentation on App Messaging: Documentation

License
This project is licensed under the MIT License
