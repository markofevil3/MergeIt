using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class InAppPurchase : MonoBehaviour {
  public static bool isPurchasing = false;
  public static InAppPurchase Instance { get; private set; }
	
	void Awake() {
    Instance = this;
  }
	
  #if UNITY_IPHONE
	  
  string[] productIdentifiers = new string[] { "jeweltheme", "candytheme" };
  private List<StoreKitProduct> products;
  
	void Start()
	{
		StoreKitBinding.requestProductData(productIdentifiers);

		StoreKitManager.transactionUpdatedEvent += transactionUpdatedEvent;
		StoreKitManager.productPurchaseAwaitingConfirmationEvent += productPurchaseAwaitingConfirmationEvent;
		StoreKitManager.purchaseSuccessfulEvent += purchaseSuccessfulEvent;
		StoreKitManager.purchaseCancelledEvent += purchaseCancelledEvent;
		StoreKitManager.purchaseFailedEvent += purchaseFailedEvent;
		StoreKitManager.productListReceivedEvent += productListReceivedEvent;
		StoreKitManager.productListRequestFailedEvent += productListRequestFailedEvent;
		StoreKitManager.restoreTransactionsFailedEvent += restoreTransactionsFailedEvent;
		StoreKitManager.restoreTransactionsFinishedEvent += restoreTransactionsFinishedEvent;
		StoreKitManager.paymentQueueUpdatedDownloadsEvent += paymentQueueUpdatedDownloadsEvent;
	}
  
  public void PurchaseProduct(int index) {
    if ( products != null && products.Count > 0 ) {
			StoreKitBinding.purchaseProduct( IndexToProductIdentifier(index), 1 );
			isPurchasing = true;
		} else {
		  StoreKitBinding.requestProductData(productIdentifiers);
		}
  }
  
  void transactionUpdatedEvent( StoreKitTransaction transaction ) {
		Debug.Log( "transactionUpdatedEvent: " + transaction );
	}

	
	void productListReceivedEvent( List<StoreKitProduct> productList ) {
	  products = productList;
		Debug.Log( "productListReceivedEvent. total products received: " + productList.Count );
		// print the products to the console
		foreach( StoreKitProduct product in productList )
			Debug.Log( product.ToString() + "\n" );
	}
	
	
	void productListRequestFailedEvent( string error ) {
		Debug.Log( "productListRequestFailedEvent: " + error );
	}
	

	void purchaseFailedEvent( string error ){
		Debug.Log( "purchaseFailedEvent: " + error );
		isPurchasing = false;
	}
	

	void purchaseCancelledEvent( string error ) {
		Debug.Log( "purchaseCancelledEvent: " + error );
		isPurchasing = false;
	}
	
	
	void productPurchaseAwaitingConfirmationEvent( StoreKitTransaction transaction ) {
		Debug.Log( "productPurchaseAwaitingConfirmationEvent: " + transaction );
	}
	
	
	void purchaseSuccessfulEvent( StoreKitTransaction transaction ) {
		Debug.Log( "purchaseSuccessfulEvent: " + transaction );
		ScreenManager.Instance.TrackEvent("InAppPurchase", "Purchase", "Success - " + transaction.productIdentifier, 1);
    // GAPurchaseItem purchasedItem = new GAPurchaseItem("Purchase", "Success - " + transaction.productIdentifier, 0.99m);
    // GoogleAnalytics.instance.Add(purchasedItem);
    // GoogleAnalytics.instance.Dispatch();
		
		isPurchasing = false;
		PlayerPrefs.SetInt(transaction.productIdentifier, 1);
		if (MainScreen.Instance != null) {
		  MainScreen.Instance.UpdatePurchasedTheme(ProductIdentifierToIndex(transaction.productIdentifier));
		}
	}
	
	private int ProductIdentifierToIndex(string identify) {
	  switch(identify) {
	    case "jeweltheme":
	      return 2;
	    break;
	    case "candytheme":
	      return 3;
	    break;
	  }
	  return 2;
	}
	
	private string IndexToProductIdentifier(int index) {
	  switch(index) {
	    case 2:
	      return "jeweltheme";
	    break;
	    case 3:
	      return "candytheme";
	    break;
	  }
	  Debug.Log("#################");
	  return "jeweltheme";
	}
	
	void restoreTransactionsFailedEvent( string error ){
		Debug.Log( "restoreTransactionsFailedEvent: " + error );
	}
	
	
	void restoreTransactionsFinishedEvent(){
		Debug.Log( "restoreTransactionsFinished" );
	}
	
	
	void paymentQueueUpdatedDownloadsEvent( List<StoreKitDownload> downloads ) {
		Debug.Log( "paymentQueueUpdatedDownloadsEvent: " );
		foreach( var dl in downloads )
			Debug.Log( dl );
	}
	#endif
  
}
