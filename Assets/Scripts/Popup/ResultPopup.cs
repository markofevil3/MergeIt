using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultPopup : Popup {
  
  public GameObject highScoreStar;
  public UILabel scoreLabel;
  public UILabel highScoreLabel;
  public UIEventTrigger btnTryAgain;
  public UIEventTrigger btnQuit;
  public UIEventTrigger btnShareFacebook;
  // public UIEventTrigger btnShareTwitter;
  public UISprite title;
  public UISprite crowSprite;
  
  private int highestTile;
  private int score;
  
  public override void Init(object[] data) {
    base.Init(data);
    GameManager.stopped = true;
		EventDelegate.Set (btnTryAgain.onClick, TryAgain);
		EventDelegate.Set (btnQuit.onClick, Quit);
		EventDelegate.Set (btnShareFacebook.onClick, ShareFacebook);
		score = (int)data[0];
		scoreLabel.text = score.ToString();
		highestTile = (int)data[1];
		bool isHighScore = (bool)data[2];
    if (isHighScore) {
      title.spriteName = "txt_newHighScore";
      highScoreStar.SetActive(true);
      
      Facebook.instance.postMessageWithLinkAndLinkToImage("You Got " + score + " points!",
                                                          "https://itunes.apple.com/us/app/power-of-2/id841898323?ls=1&mt=8",
                                                          "Power of 2",
                                                          "https://dl.dropboxusercontent.com/u/86872228/PowerOf2/logo.png", null, null);
      
    } else {
      title.spriteName = "txt_score";
      highScoreStar.SetActive(false);
    }
    title.MakePixelPerfect();
    title.transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
    crowSprite.spriteName = "icon_" + highestTile;
    highScoreLabel.text = "[5e98a8]HIGHSCORE [f48426]" + PlayerPrefs.GetInt("highScore").ToString();
  }
  
  private void ShareFacebook() {
	  #if UNITY_IPHONE
	    var parameters = new Dictionary<string,string>
			{
				{ "link", "https://itunes.apple.com/us/app/power-of-2/id841898323?ls=1&mt=8" },
				{ "name", "Power of 2" },
				{ "picture", "https://dl.dropboxusercontent.com/u/86872228/PowerOf2/logo.png" },
				{ "caption", "You Got " + score + " points!" }
			};
			FacebookBinding.showDialog( "stream.publish", parameters );
      // FacebookBinding.login();
      // FacebookBinding.showFacebookComposer("Test");
      // Facebook.instance.postMessageWithLinkAndLinkToImage("Get 5000 score in Power Of 2", "www.google.com", "Power of 2", "https://dl.dropboxusercontent.com/u/86872228/PowerOf2/logo.png", null, null);
		#endif
		#if UNITY_ANDROID
		  
		#endif
  }
  
  private void TryAgain() {
    GameManager.Instance.Close();
    Close();
    ScreenManager.Instance.OpenGameScreen(PlayerPrefs.GetInt("theme"));
  }
  
  private void Quit() {
    GameManager.Instance.Close();
    CloseNoAnimation();
    ScreenManager.Instance.OpenMainScreen();
  }
}
