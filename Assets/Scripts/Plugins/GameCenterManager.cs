using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;
#if UNITY_ANDROID
using GooglePlayGames;
#endif

public class GameCenterManager : MonoBehaviour {

	public static GameCenterManager Instance { get; private set; }
	private bool isLoggedIn = false;
	private string IOS_LEADERBOARD_ID = "highest_score";
	private string ANDROID_LEADERBOARD_ID = "CgkIv6_F7r8GEAIQAQ";
  
  #if UNITY_IPHONE
    void Awake() {
      Instance = this;
      Login();
    }
  #endif
  #if UNITY_ANDROID
    void Awake() {
      Instance = this;
      // recommended for debugging:
      PlayGamesPlatform.DebugLogEnabled = true;

      // Activate the Google Play Games platform
      PlayGamesPlatform.Activate();
    }
  #endif
  private void Login() {
    Social.localUser.Authenticate (success => {
  		if (success) {
  			Debug.Log ("Authentication successful");
  			string userInfo = "Username: " + Social.localUser.userName + 
  				"\nUser ID: " + Social.localUser.id + 
  				"\nIsUnderage: " + Social.localUser.underage;
  			Debug.Log (userInfo);
  			isLoggedIn = true;
  		}
  		else
  			Debug.Log ("Authentication failed");
  	});
  }

  public void ShowLeaderboard() {
    if (isLoggedIn) {
      Social.ShowLeaderboardUI();
    } else {
      Login();
    }
  }

  public void RegisterScore(int score) {
    #if UNITY_IPHONE
      Social.ReportScore (score, IOS_LEADERBOARD_ID, success => {
    		Debug.Log(success ? "Reported score successfully" : "Failed to report score");
  		});
    #endif
    #if UNITY_ANDROID
      Social.ReportScore (score, ANDROID_LEADERBOARD_ID, success => {
    		Debug.Log(success ? "Reported score successfully" : "Failed to report score");
  		});
    #endif
  }
}
