using UnityEngine;
using System.Collections;

public class AdsManager : MonoBehaviour {
  
  public static AdsManager Instance { get; private set; }
  
  private string IOS_IPHONE_KEY = "a153258928c68c6";
  private string IOS_IPAD_KEY = "a15325a3e5e9d2c";
  private string ANDROID_KEY = "a15329210136de3";
  
  void Awake() {
    Instance = this;
  }
  
  public void ShowAds() {
    string extras = "{\"color_bg\":\"AAAAFF\", \"color_bg_top\":\"FFFFFF\"}";
		#if UNITY_IPHONE
		  if (Utils.IsTablet()) {
	      AdMobPlugin.CreateBannerView(IOS_IPAD_KEY, AdMobPlugin.AdSize.Leaderboard, true);
	      AdMobPlugin.RequestBannerAd(false, extras);
	    } else {
	      AdMobPlugin.CreateBannerView(IOS_IPHONE_KEY, AdMobPlugin.AdSize.SmartBanner, true);
	      AdMobPlugin.RequestBannerAd(false, extras);
	    }
		#endif
		#if UNITY_ANDROID
			GoogleMobileAdsPlugin.CreateBannerView(ANDROID_KEY, GoogleMobileAdsPlugin.AdSize.Banner, true);
			GoogleMobileAdsPlugin.RequestBannerAd(true);
		#endif
  }

  #if UNITY_IPHONE
		public void HideAds() {
	    AdMobPlugin.HideBannerView();
	  }
	  void OnEnable(){
	    AdMobPlugin.ReceivedAd += HandleReceivedAd;
	    AdMobPlugin.FailedToReceiveAd += HandleFailedToReceiveAd;
	    AdMobPlugin.ShowingOverlay += HandleShowingOverlay;
	    AdMobPlugin.DismissingOverlay += HandleDismissingOverlay;
	    AdMobPlugin.DismissedOverlay += HandleDismissedOverlay;
	    AdMobPlugin.LeavingApplication += HandleLeavingApplication;
	  }
   #endif

	#if UNITY_ANDROID
		public void HideAds() {
		  if (Application.platform == RuntimePlatform.Android) {
		    GoogleMobileAdsPlugin.HideBannerView();
	 		}
	  }
	  void OnEnable() {
		  GoogleMobileAdsPlugin.ReceivedAd += HandleReceivedAd;
      GoogleMobileAdsPlugin.FailedToReceiveAd += HandleFailedToReceiveAd;
      GoogleMobileAdsPlugin.ShowingOverlay += HandleShowingOverlay;
      GoogleMobileAdsPlugin.DismissedOverlay += HandleDismissedOverlay;
      GoogleMobileAdsPlugin.LeavingApplication += HandleLeavingApplication;
	  }
	#endif
  
  public void HandleReceivedAd(){
    print("HandleReceivedAd event received");
  }

  public void HandleFailedToReceiveAd(string message){
    print("HandleFailedToReceiveAd event received with message:");
    print(message);
  }

  public void HandleShowingOverlay(){
  }

  public void HandleDismissingOverlay(){}

  public void HandleDismissedOverlay(){
  }
  
  public void HandleLeavingApplication(){
  }
}
