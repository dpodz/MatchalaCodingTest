using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The IAPManager is the interface through which all IAPs are sent and processed.
/// It is set up to be used with UnityEngine.Purchasing
/// </summary>
public class IAPManager : MonoBehaviour
{
    // Product identifiers
    static readonly string Subscription_Monthly = "SUB_MONTHLY";
    static readonly string Subscription_Yearly = "SUB_YEARLY";

    // Instance management
    [Header("Instance Management")]
    public GameObject loadingWindow;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void InitializePurchasing()
    {

    }

    private bool IsInitialized()
    {
        return true;
    }

    public void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            if (productId == Subscription_Monthly)
            {
                Debug.Log("BuyProductID Success: Asking to subscribe to monthly... ");
                StartCoroutine(ProcessPurchase(Subscription_Monthly));
            }
            else if (productId == Subscription_Yearly)
            {
                Debug.Log("BuyProductID Success: Asking to subscribe to yearly... ");
                StartCoroutine(ProcessPurchase(Subscription_Yearly));
            }
            else
            {
                Debug.Log("BuyProductID FAIL: Product not found");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public IEnumerator ProcessPurchase(string productId)
    {
        loadingWindow.SetActive(true);
        yield return new WaitForSeconds(2f);
        loadingWindow.SetActive(false);

        if (IsInitialized())
        {
            if (productId == Subscription_Monthly)
            {
                Debug.Log("ProcessPurchase Success: Subscribed monthly!");
                ContentManager.instance.UnlockNewContent(Content.ContentId_Subscription);
            }
            else if (productId == Subscription_Yearly)
            {
                Debug.Log("ProcessPurchase Success: Subscribed yearly!");
                ContentManager.instance.UnlockNewContent(Content.ContentId_Subscription);
            }
            else
            {
                Debug.Log("ProcessPurchase FAIL: Product not found");
            }
        }
        else
        {
            Debug.Log("ProcessPurchase FAIL. Not initialized.");
        }
    }

    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        ContentManager.instance.RestoreContent();

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            /*
            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
            */
        }
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    // !!!!!
    // EVERYTHING FROM THIS POINT ON IS LEFTOVER FOR USE IF ACTUALLY IMPLEMENTING UnityEngine.Purchasing!
    // !!!!!

    //  
    // --- IStoreListener
    //
    /*
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, Donation_2D, StringComparison.Ordinal) ||
            String.Equals(args.purchasedProduct.definition.id, Donation_5D, StringComparison.Ordinal) ||
            String.Equals(args.purchasedProduct.definition.id, Donation_10D, StringComparison.Ordinal) ||
            String.Equals(args.purchasedProduct.definition.id, CostumePack, StringComparison.Ordinal) ||
            String.Equals(args.purchasedProduct.definition.id, CelestialPack, StringComparison.Ordinal) ||
            String.Equals(args.purchasedProduct.definition.id, CowboySkin, StringComparison.Ordinal) ||
            String.Equals(args.purchasedProduct.definition.id, CourierSkin, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // redeem unlock code
            if (StoreManager.instance != null)
            {
                StoreManager.instance.RedeemUnlockCode(args.purchasedProduct.definition.id);
            }
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            StoreManager.instance.ShowPurchaseErrorMessage("ProcessPurchase FAIL: Product '" + args.purchasedProduct.definition.id + "' not recognized.");
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        StoreManager.instance.ShowPurchaseErrorMessage(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
    */
}
