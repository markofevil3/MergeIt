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
  public UIButton shareFacebookUIButton;
  // public UIEventTrigger btnShareTwitter;
  public UISprite title;
  public UISprite crowSprite;
  public AudioClip endGameSound;
  public AudioClip highScoreSound;
  
  private int highestTile;
  private int score;
  
  public override void Init(object[] data) {
    base.Init(data);
    ScreenManager.Instance.TrackScreen("ResultScreen");
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
      if (ScreenManager.internetAvailable) {
        #if UNITY_IPHONE
          var permissions = new string[] { "publish_actions", "publish_stream" };
    			FacebookBinding.reauthorizeWithPublishPermissions( permissions, FacebookSessionDefaultAudience.Everyone );
    			
          var parameters = new Dictionary<string,string>
          {
            { "link", "http://www.google.com" },
            { "name", "Power of 2" },
            { "picture", "https://dl.dropboxusercontent.com/u/86872228/PowerOf2/logo.png" },
            { "caption", "I got " + score + " points! Can you beat my score?" }
          };
          FacebookBinding.showDialog( "stream.publish", parameters );
        #endif
        #if UNITY_ANDROID
          var parameters = new Dictionary<string,string>
          {
            { "link", "http://play.google.com/store/apps/details?id=com.buiphiquan.powerof2" },
            { "name", "Power of 2" },
            { "picture", "https://dl.dropboxusercontent.com/u/86872228/PowerOf2/logo.png" },
            { "caption", "I got " + score + " points! Can you beat my score?" }
          };
          FacebookAndroid.showDialog( "stream.publish", parameters );
        #endif
      }
    } else {
      title.spriteName = "txt_score";
      highScoreStar.SetActive(false);
    }
    title.MakePixelPerfect();
    crowSprite.spriteName = "icon_" + highestTile;
    highScoreLabel.text = "[5e98a8]HIGHSCORE [f48426]" + PlayerPrefs.GetInt("highScore").ToString();
    if (isHighScore) {
      NGUITools.PlaySound(highScoreSound, 1);
    } else {
      NGUITools.PlaySound(endGameSound, 1);
    }
  }
  
  private void ShareFacebook() {
    //     #if UNITY_IPHONE
    // Facebook.instance.postMessageWithLinkAndLinkToImage("I got " + score + " points! Can you beat my score?",
    //                                                         "https://itunes.apple.com/us/app/power-of-2/id841898323?ls=1&mt=8",
    //                                                         "Power of 2",
    //                                                         "https://dl.dropboxusercontent.com/u/86872228/PowerOf2/logo.png", null, null);
    //     #endif
    //     #if UNITY_ANDROID
    // Facebook.instance.postMessageWithLinkAndLinkToImage("I got " + score + " points! Can you beat my score?",
    //                                                         "http://play.google.com/store/apps/details?id=com.buiphiquan.powerof2",
    //                                                         "Power of 2",
    //                                                         "https://dl.dropboxusercontent.com/u/86872228/PowerOf2/logo.png", null, null);
    //     #endif
    // shareFacebookUIButton.isEnabled = false;
    if (ScreenManager.internetAvailable) {
      shareFacebookUIButton.isEnabled = true;
      #if UNITY_IPHONE
        var parameters = new Dictionary<string,string>
        {
          { "link", "http://www.google.com" },
          { "name", "Power of 2" },
          { "picture", "https://dl.dropboxusercontent.com/u/86872228/PowerOf2/logo.png" },
          { "caption", "I got " + score + " points! Can you beat my score?" }
        };
        FacebookBinding.showDialog( "stream.publish", parameters );
      #endif
      #if UNITY_ANDROID
        var parameters = new Dictionary<string,string>
        {
          { "link", "http://play.google.com/store/apps/details?id=com.buiphiquan.powerof2" },
          { "name", "Power of 2" },
          { "picture", "https://dl.dropboxusercontent.com/u/86872228/PowerOf2/logo.png" },
          { "caption", "I got " + score + " points! Can you beat my score?" }
        };
        FacebookAndroid.showDialog( "stream.publish", parameters );
      #endif
    } else {
      shareFacebookUIButton.isEnabled = false;
    }
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
