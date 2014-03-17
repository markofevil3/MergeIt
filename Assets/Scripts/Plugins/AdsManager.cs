using UnityEngine;
using System.Collections;

public class AdsManager : MonoBehaviour {
  
  public static AdsManager Instance { get; private set; }
  
  private string IOS_IPHONE_KEY = "a153258928c68c6";
  private string IOS_IPAD_KEY = "a15325a3e5e9d2c";
  
  void Awake() {
    Instance = this;
  }
  
  public void ShowAds() {
    string extras = "{\"color_bg\":\"AAAAFF\", \"color_bg_top\":\"FFFFFF\"}";
    if (Utils.IsTablet()) {
      AdMobPlugin.CreateBannerView(IOS_IPAD_KEY, AdMobPlugin.AdSize.Leaderboard, true);
      AdMobPlugin.RequestBannerAd(true, extras);
    } else {
      AdMobPlugin.CreateBannerView(IOS_IPHONE_KEY, AdMobPlugin.AdSize.SmartBanner, true);
      AdMobPlugin.RequestBannerAd(true, extras);
    }
  }
  
  void HideAds() {
    
  }
  
  void OnEnable(){
    AdMobPlugin.ReceivedAd += HandleReceivedAd;
    AdMobPlugin.FailedToReceiveAd += HandleFailedToReceiveAd;
    AdMobPlugin.ShowingOverlay += HandleShowingOverlay;
    AdMobPlugin.DismissingOverlay += HandleDismissingOverlay;
    AdMobPlugin.DismissedOverlay += HandleDismissedOverlay;
    AdMobPlugin.LeavingApplication += HandleLeavingApplication;
  }
  
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
