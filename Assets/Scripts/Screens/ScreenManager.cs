using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {

	public static ScreenManager Instance { get; private set; }
	
	public GameObject parent;
	public GameObject mainScreenPrefab;
	public GameObject inGameSreenPrefab;
	
	public MainScreen mainScreenScript;
	public GameManager gameManagerScript;
	
	public bool isFbInit = false;

	// Use this for initialization
	void Awake () {
		Instance = this;
		Application.targetFrameRate = 60;
		Application.runInBackground = false;
		useGUILayout = false;
		
		AtlasManager.Instance.Init();
		
		OpenMainScreen();
		FacebookInit();
	}
	
	void FacebookInit() {
	  #if UNITY_IPHONE
    FacebookBinding.init();
    #endif
		#if UNITY_ANDROID
      // InAppBillingAndroid.Init();
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
		  AtlasManager.Instance.SwitchTheme(theme);
		  PlayerPrefs.SetInt("theme", theme);
			GameObject tempGameObject = NGUITools.AddChild(parent, inGameSreenPrefab);
			tempGameObject.name = "InGameSreen";
			gameManagerScript = tempGameObject.GetComponent<GameManager>();
			gameManagerScript.Init();
		}
	}
}
