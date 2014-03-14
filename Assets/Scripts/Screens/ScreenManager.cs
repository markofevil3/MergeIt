using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {

	public static ScreenManager Instance { get; private set; }
	
	public GameObject parent;
	public GameObject mainScreenPrefab;
	public GameObject inGameSreenPrefab;

	// Use this for initialization
	void Awake () {
		Instance = this;
		OpenMainScreen();
	}
	
	public void OpenMainScreen() {
		GameObject tempGameObject = NGUITools.AddChild(parent, mainScreenPrefab);
		tempGameObject.name = "MainScreen";
		tempGameObject.GetComponent<BaseScreen>().Init();
	}
	
	public void OpenGameScreen() {
		GameObject tempGameObject = NGUITools.AddChild(parent, inGameSreenPrefab);
		tempGameObject.name = "InGameSreen";
		tempGameObject.GetComponent<BaseScreen>().Init();
	}
}
