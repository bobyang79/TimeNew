using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoogleMobileAdsDemoScriptBanner : MonoBehaviour
{
    private BannerView bannerView;
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }
    private void RequestBanner() {
        //string adUnitId = "ca-app-pub-7818882699083478/4350649643"; // japaneses
        //string adUnitId = "ca-app-pub-3940256099942544/6300978111"; //Example
        string adUnitId = "ca-app-pub-7818882699083478/7416907745"; // For game
        // Create a 320x50 banner at the top of the screen.

        if (Screen.height <= Screen.width * 16 / 9) {
            return;
        }

        AdSize size = new AdSize(Screen.width, (Screen.height - Screen.width * 16 / 9)/2);
        //size = AdSize.MediumRectangle;
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
      
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}