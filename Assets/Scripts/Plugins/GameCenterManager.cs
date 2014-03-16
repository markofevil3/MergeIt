using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class GameCenterManager : MonoBehaviour {

	public static GameCenterManager Instance { get; private set; }
	private bool isLoggedIn = false;
	private string leaderboardID = "highest_score";

  void Awake() {
    Instance = this;
    Login();
  }
  
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
    // if (isLoggedIn) {
      // Social.ShowLeaderboardUI();
    ILeaderboard leaderboard = Social.CreateLeaderboard();
    	leaderboard.id = leaderboardID;
    	leaderboard.LoadScores (result => {				
    		Debug.Log("Received " + leaderboard.scores.Length + " scores");
    		foreach (IScore score in leaderboard.scores)
    			Debug.Log(score);
    	});
    // }
  }
  
  public void RegisterScore(int score) {
    Social.ReportScore (score, leaderboardID, success => {
  		Debug.Log(success ? "Reported score successfully" : "Failed to report score");
		});
  }
}
