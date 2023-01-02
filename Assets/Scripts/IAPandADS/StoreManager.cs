using HmsPlugin;
using HuaweiMobileServices.IAP;
using HuaweiMobileServices.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class StoreManager : MonoBehaviour
{
    public static Action<string> IAPLog;

    public GameObject product1, product2, product3;

    private IList<ProductInfoResult> productInfos = new List<ProductInfoResult>();

    private int Counter = 0;

    public bool isIAPavailable { get; private set; }

    private void Start()
    {
        Debug.Log("!!!!!!!!!!!!!!!!!!!STORE MANAGER ÝÇÝNDE!!!!!!!!!!!!!!!!!!!!");
        isIAPavailable = false;
    }

    public void BuyProduct(string productId)
    {
        HMSIAPManager.Instance.OnBuyProductSuccess = OnBuyProductSuccess;

        HMSIAPManager.Instance.OnCheckIapAvailabilitySuccess += OnCheckIapAvailabilitySuccess;
        HMSIAPManager.Instance.OnCheckIapAvailabilityFailure += OnCheckIapAvailabilityFailure;

        HMSIAPManager.Instance.OnBuyProductFailure = OnBuyProductFailure;
        HMSIAPManager.Instance.BuyProduct(productId);
    }
    private void OnBuyProductFailure(int obj)
    {
        Debug.LogError("OnBuyProductFailure:" + obj);

        if (obj == OrderStatusCode.ORDER_PRODUCT_OWNED)
        {
            HMSIAPManager.Instance.OnObtainOwnedPurchasesSuccess = OnObtainOwnedPurchasesSuccess;
            HMSIAPManager.Instance.ObtainOwnedPurchases(PriceType.IN_APP_NONCONSUMABLE);
        }
    }

    private void OnBuyProductSuccess(PurchaseResultInfo obj)
    {
        Debug.Log("OnBuyProductSuccess:" + obj.InAppPurchaseData.ProductName);

        if (obj.InAppPurchaseData.ProductId == HMSIAPConstants.NoAds)
        {
            AdsManager adsManager = gameObject.AddComponent<AdsManager>();
            adsManager.DisableAds();
            
            
        }
        else if (obj.InAppPurchaseData.ProductId == HMSIAPConstants.Booster)
        {
            Debug.Log("this is your booster");
        }
        else if (obj.InAppPurchaseData.ProductId == HMSIAPConstants.PinkColor)
        {
            BalloonColor balloonColor = new BalloonColor();
            balloonColor.UnlockColor();
        }
    }

    //SOR!!
    private void OnObtainOwnedPurchasesSuccess(OwnedPurchasesResult result)
    {
        if (result != null)
        {
            foreach (var obj in result.InAppPurchaseDataList)
            {
                HMSIAPManager.Instance.ConsumePurchaseWithToken(obj.PurchaseToken);
            }
        }
    }

    internal void InitIAP()
    {
        //Uncheck Init on start box in IAP page
        HMSIAPManager.Instance.OnCheckIapAvailabilitySuccess = OnCheckIapAvailabilitySuccess;
        HMSIAPManager.Instance.OnObtainProductInfoSuccess = OnObtainProductInfoSuccess;
        HMSIAPManager.Instance.CheckIapAvailability();
    }

    public void OnObtainProductInfoSuccess(IList<ProductInfoResult> list)
    {
        Debug.Log("productInfos infoyu dolduruyoruz :)" + list.Count);
        if(list != null)
        {
            foreach (var item in list)
            {
                productInfos.Add(item);

            }

        }
        


        
    }

    public void FillProducts()
    {
        if (productInfos == null)
        {
            Debug.Log("PRODUCT INFOS NULL MALESEF");
        }
        foreach (ProductInfoResult res in productInfos)
            for (int i = 0; i < res.ProductInfoList.Count; i++)
            {
                switch (Counter++)
                {
                    case 0:
                        FillProduct(product1, res.ProductInfoList[i]);
                        break;
                    case 1:
                        FillProduct(product2, res.ProductInfoList[i]);
                        break;
                    case 2:
                        FillProduct(product3, res.ProductInfoList[i]);
                        break;
                }
            }
    }

    private void FillProduct(GameObject product, ProductInfo productInfo)
    {
        Debug.Log("FillProduct" + product.name + " - productInfo" + productInfo.ProductName);
        product.SetActive(true);
        product.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = productInfo.ProductName;
        product.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = productInfo.ProductDesc;
        product.transform.GetChild(3).gameObject.GetComponent<TextMeshPro>().text = productInfo.Price + productInfo.Currency;
        product.transform.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(delegate { BuyProduct(productInfo.ProductId); });
    }
    private void OnCheckIapAvailabilityFailure(HMSException obj)
    {
        //isIAPavailable = false;
        IAPLog?.Invoke("IAP is not ready.");
    }

    private void OnCheckIapAvailabilitySuccess()
    {
        isIAPavailable = true;
        IAPLog?.Invoke("IAP is ready.");
    }

}
