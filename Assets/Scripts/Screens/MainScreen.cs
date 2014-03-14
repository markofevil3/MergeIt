using UnityEngine;
using System.Collections;

public class MainScreen : BaseScreen {

	public UIEventTrigger btnPlay;

	public override void Init() {
		EventDelegate.Set (btnPlay.onClick, OpenGameScreen);
	}
	
	void OpenGameScreen() {
		Close();
		ScreenManager.Instance.mainScreenScript = null;
		ScreenManager.Instance.OpenGameScreen();
	}
	
}
