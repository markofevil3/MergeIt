using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InAppPurchaseAndroid : MonoBehaviour {
  #if UNITY_ANDROID
	
  public static InAppPurchaseAndroid Instance { get; private set; }
  
  private string key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2CRPVXDZInQiXYGOz+xAxYAKOFUlD6RUM7gDVaMPK39mnBkzZZh5pseSzPu0x3n8r+HTw85sg/kzVxD3Pr0j7xkHgZb6mUtmX0tQPlcrIw5ib+V93U0g+o6XCrkBTYW2lEqhWYRBQxa3Uia2Py9al3Cj32/CglGrxWnZ/UO5N4ZnqfveGfWeqM1IyRNSdxUOAUQGQSiwtC+yszTF16uOiaRLvd74dr8qIp/PpBp1Cu/B+TMne/eYF/FZNDJWtK9DONQQMUA+YsY5f8rQaoGQWxeM93dOCMEQ2JCN3eM9yZecaPbesGNd6FTWLq9VQ/NIbYWb7NvTnVlxvXb8D7buCwIDAQAB";
  
  string[] productIdentifiers = new string[] { "com.buiphiquan.candytheme", "com.buiphiquan.jeweltheme" };
  private bool finishedQueryInventory = false;
  
  void Start() {
    Instance = this;
    GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		
    GoogleIAB.init(key);
    GoogleIAB.areSubscriptionsSupported();
  }
  
  public void PurchaseProduct(int index) {
    if (finishedQueryInventory) {
      GoogleIAB.purchaseProduct(IndexToProductIdentifier(index));
  		InAppPurchase.isPurchasing = true;
    } else {
      GoogleIAB.queryInventory(productIdentifiers);
    }
  }
  
  private string IndexToProductIdentifier(int index) {
	  switch(index) {
	    case 2:
	      return productIdentifiers[0];
	    break;
	    case 3:
        return productIdentifiers[1];
	    break;
	  }
	  Debug.Log("#################");
	  return productIdentifiers[0];
	}
	
	void billingSupportedEvent()
	{
	  GoogleIAB.queryInventory(productIdentifiers);
		Debug.Log( "billingSupportedEvent" );
	}

	void billingNotSupportedEvent( string error )
	{
		Debug.Log( "billingNotSupportedEvent: " + error );
	}


	void queryInventorySucceededEvent( List<GooglePurchase> purchases, List<GoogleSkuInfo> skus )
	{
		Debug.Log( string.Format( "queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count ) );
		finishedQueryInventory = true;
		Prime31.Utils.logObject( purchases );
		Prime31.Utils.logObject( skus );
	}


	void queryInventoryFailedEvent( string error )
	{
		Debug.Log( "queryInventoryFailedEvent: " + error );
	}


	void purchaseCompleteAwaitingVerificationEvent( string purchaseData, string signature )
	{
		Debug.Log( "purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature );
	}


	void purchaseSucceededEvent( GooglePurchase purchase )
	{
		InAppPurchase.isPurchasing = false;
		ScreenManager.Instance.TrackEvent("InAppPurchase", "Purchase", "Success - " + purchase.packageName, 1);
		
    // GAPurchaseItem purchasedItem = new GAPurchaseItem("Purchase", "Success - " + purchase.packageName, 0.99m);
    // GoogleAnalytics.instance.Add(purchasedItem);
    // GoogleAnalytics.instance.Dispatch();
		
		PlayerPrefs.SetInt(GetSaveKey(purchase.packageName), 1);
		if (MainScreen.Instance != null) {
		  MainScreen.Instance.UpdatePurchasedTheme(ProductIdentifierToIndex(purchase.packageName));
		}
		
		Debug.Log( "purchaseSucceededEvent: " + purchase );
	}

  private string GetSaveKey(string identify) {
    switch(identify) {
	    case "com.buiphiquan.candytheme":
	      return "candytheme";
	    break;
	    case "com.buiphiquan.jeweltheme":
	      return "jeweltheme";
	    break;
	  }
	  return "candytheme";
  }

	private int ProductIdentifierToIndex(string identify) {
	  switch(identify) {
	    case "com.buiphiquan.candytheme":
	      return 2;
	    break;
	    case "com.buiphiquan.jeweltheme":
	      return 3;
	    break;
	  }
	  return 2;
	}

	void purchaseFailedEvent( string error )
	{
		InAppPurchase.isPurchasing = false;
		Debug.Log( "purchaseFailedEvent: " + error );
	}
	void consumePurchaseSucceededEvent( GooglePurchase purchase )
	{
		Debug.Log( "consumePurchaseSucceededEvent: " + purchase );
	}
	void consumePurchaseFailedEvent( string error )
	{
		Debug.Log( "consumePurchaseFailedEvent: " + error );
	}
#endif
}
