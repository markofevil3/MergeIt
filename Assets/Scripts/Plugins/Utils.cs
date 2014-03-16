using UnityEngine;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Text;

public class Utils : MonoBehaviour {
  
  public static bool IsTablet() {
		#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return SystemInfo.deviceModel.IndexOf("ipad", StringComparison.OrdinalIgnoreCase) > -1;
			}
		#endif
		#if UNITY_ANDROID
			if (Application.platform == RuntimePlatform.Android) {
				DisplayMetricsAndroid.Init();
				return DisplayMetricsAndroid.IsTablet();
			}
		#endif
		return true;
	}
  
}
