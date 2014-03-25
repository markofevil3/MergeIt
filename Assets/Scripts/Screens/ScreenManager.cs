using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {

	public static ScreenManager Instance { get; private set; }
	
	public GameObject parent;
	public GameObject mainScreenPrefab;
	public GameObject theme1Prefab;
	public GameObject theme2Prefab;
	public GameObject theme3Prefab;
	
	public MainScreen mainScreenScript;
	public GameManager gameManagerScript;
	public AtlasManager atlasManager;
	public CGoogleAnalytics ga;
	
	public static bool internetAvailable = false;
	
	public bool isFbInit = false;

	// Use this for initialization
	void Awake () {
		Instance = this;
		Application.targetFrameRate = 60;
		Application.runInBackground = false;
		useGUILayout = false;
		
		atlasManager.Init();
	}
	
	void Start() {
    TrackScreen("MainScreen");
		FacebookInit();
		OpenMainScreen();
	}
	
	public void TrackScreen(string screenName) {
  	// session starts
		ga.analytics.TrackSession(true);
		// tracking screens
		ga.analytics.TrackAppview(screenName);
		// session ends
		ga.analytics.TrackSession(false);
	}

	public void TrackEvent(string category, string label, string action, int value) {
	  // session starts
		ga.analytics.TrackSession(true);
		// tracking screens
		ga.analytics.TrackEvent(category, label, action, value);
		// session ends
		ga.analytics.TrackSession(false);
	}
	
	void FacebookInit() {
	  #if UNITY_IPHONE
    FacebookBinding.init();
    #endif
		#if UNITY_ANDROID
    FacebookAndroid.init();
		#endif
	}
	
	void FacebookInitCallback() {
	  isFbInit = true;
	}
	
	public void OpenMainScreen() {
		if (mainScreenScript == null) {
		  GameManager.started = false;
			GameObject tempGameObject = NGUITools.AddChild(parent, mainScreenPrefab);
			tempGameObject.name = "MainScreen";
			mainScreenScript = tempGameObject.GetComponent<MainScreen>();
			mainScreenScript.Init();
		}
	}
	
	public void OpenGameScreen(int theme) {
		if (gameManagerScript == null) {
		  PlayerPrefs.SetInt("theme", theme);
		  GameObject tempGameObject;
		  switch(theme) {
		    case 0:
		      tempGameObject = NGUITools.AddChild(parent, theme1Prefab);
		    break;
		    case 1:
		      tempGameObject = NGUITools.AddChild(parent, theme2Prefab);
		    break;
		    case 2:
		      tempGameObject = NGUITools.AddChild(parent, theme3Prefab);
		    break;
		    default:
		      tempGameObject = NGUITools.AddChild(parent, theme1Prefab);
		    break;
		  }
      // AtlasManager.Instance.SwitchTheme(theme);
			tempGameObject.name = "InGameSreen";
			gameManagerScript = tempGameObject.GetComponent<GameManager>();
			gameManagerScript.Init(theme);
		}
	}
}
