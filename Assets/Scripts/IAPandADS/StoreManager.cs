using HuaweiMobileServices.IAP;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HmsPlugin;

public class StoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyProduct(string productId)
    {
        HMSIAPManager.Instance.OnBuyProductSuccess = OnBuyProductSuccess;
        HMSIAPManager.Instance.OnBuyProductFailure = OnBuyProductFailure;
        HMSIAPManager.Instance.BuyProduct(productId);
    }
    private void OnBuyProductFailure(int obj)
    {
        Debug.LogError("OnBuyProductFailure:" + obj);
    }

    private void OnBuyProductSuccess(PurchaseResultInfo obj)
    {
        Debug.Log("OnBuyProductSuccess:" + obj.InAppPurchaseData.ProductName);
        if (obj.InAppPurchaseData.ProductId == HMSIAPConstants.NoAds)
        {
            
            HMSAdsKitManager.Instance.HideBannerAd();
            
        }
        else if (obj.InAppPurchaseData.ProductId == HMSIAPConstants.DoubleScore)
        {
            
        }
        else if (obj.InAppPurchaseData.ProductId == HMSIAPConstants.PinkColor)
        {
           
        }
    }
}
