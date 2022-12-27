using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsDisabler : MonoBehaviour
{
    public bool IsSubscriptionActive { get; private set; }

    public void DisableAds()
    {
        IsSubscriptionActive = true;
    }

    public void ShowAds()
    {
        IsSubscriptionActive = false;
    }
}

/*
 / Find the AdDisabler game object and get the script component
AdDisabler adDisabler = GameObject.Find("AdDisabler").GetComponent<AdDisabler>();

// Disable the ads
adDisabler.DisableAds();

// Check if the subscription is active
if (adDisabler.IsSubscriptionActive)
{
    // The subscription is active, so don't show ads
    Debug.Log("Ads are disabled");
}
else
{
    // The subscription is not active, so show ads
    Debug.Log("Ads are enabled");
}
*/