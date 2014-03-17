using UnityEngine;
using System.Collections;

public class AtlasManager : MonoBehaviour {
  
  public static AtlasManager Instance { get; private set; }
  
  public UIAtlas uiRef;
  public UIAtlas inGameUIRef;
  
  private string uiAtlasName;
  private string inGameAtlasTheme1Name;
  private string inGameAtlasTheme2Name;
  private string inGameAtlasTheme3Name;
  private string inGameAtlasTheme4Name;

  void Awake() {
    Instance = this;
  }

  public void Init() {
    if (Utils.IsUHD()) {
      uiAtlasName = "UHD";
    } else {
      uiAtlasName = "HD";
    }
    uiRef.replacement = (Resources.Load("Atlas/" + uiAtlasName + "/" + uiAtlasName, typeof(GameObject)) as GameObject).GetComponent<UIAtlas>();
    // inGameUIRef.replacement = (Resources.Load("Atlas/UHD/UHD", typeof(GameObject)) as GameObject).GetComponent<UIAtlas>();
  }
  
  public void SwitchTheme(int theme) {
    
  }
}
