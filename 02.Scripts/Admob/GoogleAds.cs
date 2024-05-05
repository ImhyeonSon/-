using UnityEngine;

public class GoogleAds : MonoBehaviour
{ 

}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds;
//using GoogleMobileAds.Api;

//public class GoogleAds : MonoBehaviour
//{
//    // Start is called before the first frame update

//    void Start()
//    {
//        // Initialize the Google Mobile Ads SDK.
//        MobileAds.Initialize((InitializationStatus initStatus) =>
//        {
//            // This callback is called once the MobileAds SDK is initialized.
//        });
//        CreateBannerView();
//        LoadAd();
//    }

//#if UNITY_ANDROID
//    private string _adUnitId = "ca-app-pub-5637949611981392/9325772126";
//#elif UNITY_IPHONE
//  private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
//#else
//  private string _adUnitId = "unused";
//#endif

//    BannerView _bannerView;

//    /// <summary>
//    /// Creates a 320x50 banner view at top of the screen.
//    /// </summary>
//    public void CreateBannerView()
//    {
//        // If we already have a banner, destroy the old one.
//        if (_bannerView != null)
//        {
//            this._bannerView.Destroy();
//            DestroyBannerView();
//        }
//        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

//        // Create a 320x50 banner at top of the screen
//        _bannerView = new BannerView(_adUnitId, adaptiveSize, AdPosition.Bottom);
//    }

//    public void LoadAd()
//    {
//        // create an instance of a banner view first.
//        if (_bannerView == null)
//        {
//            CreateBannerView();
//        }

//        // create our request used to load the ad.
//        var adRequest = new AdRequest();
//        _bannerView.LoadAd(adRequest);
//    }

//    public void DestroyBannerView() // 씬 이동시 삭제
//    {
//        if (_bannerView != null)
//        {
//            _bannerView.Destroy();
//            _bannerView = null;
//        }
//    }

//    //    private string adUnitId;
//    //    // Update is called once per frame
//    //    private void RequestBanner()
//    //    {
//    //#if UNITY_ANDROID
//    //        adUnitId = "ca-app-pub-3940256099942544/6300978111";
//    //#elif UNITY_IPHONE
//    //  adUnitId = "ca-app-pub-3940256099942544/2934735716";
//    //#elif UNITY_EDITOR
//    //  adUnitId = "ca-app-pub-3940256099942544/6300978111";   
//    //#endif

//    //        if (this.bannerView != null)
//    //        {
//    //            this.bannerView.Destroy();
//    //        }


//    //        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

//    //        AdRequest request = new AdRequest();
//    //        bannerView.Show();
//    //        this.interstitial.Show
//    //        this.bannerView.LoadAd(request);
//    //    }


//    //RewardedAd _rewardedAd;

//    ///// <summary>
//    ///// Loads the rewarded ad.
//    ///// </summary>
//    //public void LoadRewardedAd()
//    //{
//    //    // Clean up the old ad before loading a new one.
//    //    if (_rewardedAd != null)
//    //    {
//    //        _rewardedAd.Destroy();
//    //        _rewardedAd = null;
//    //    }

//    //    Debug.Log("Loading the rewarded ad.");

//    //    // create our request used to load the ad.
//    //    var adRequest = new AdRequest();

//    //    // send the request to load the ad.
//    //    RewardedAd.Load(adUnitId, adRequest,
//    //        (RewardedAd ad, LoadAdError error) =>
//    //        {
//    //            // if error is not null, the load request failed.
//    //            if (error != null || ad == null)
//    //            {
//    //                Debug.LogError("Rewarded ad failed to load an ad " +
//    //                               "with error : " + error);
//    //                return;
//    //            }

//    //            Debug.Log("Rewarded ad loaded with response : "
//    //                      + ad.GetResponseInfo());

//    //            _rewardedAd = ad;
//    //        });
//    //}
//}
