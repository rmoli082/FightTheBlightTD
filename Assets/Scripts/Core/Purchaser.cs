using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour, IStoreListener
{

    private static IStoreController m_storeController;
    private static IExtensionProvider m_storeExtensionProvider;

    public static string pileOfGems = "gempile";
    public static string jarOfGems = "gemjar";
    public static string bagOfGems = "gembag";
    public static string sackOfGems = "gemsack";
    public static string boxOfGems = "gembox";

    private static readonly int pileGemAmount = 300;
    private static readonly int jarGemAmount = 635;
    private static readonly int bagGemAmount = 1300;
    private static readonly int sackGemAmount = 2600;
    private static readonly int boxGemAmount = 7500;

    private void Start()
    {
        if (m_storeController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(pileOfGems, ProductType.Consumable);
        builder.AddProduct(jarOfGems, ProductType.Consumable);
        builder.AddProduct(bagOfGems, ProductType.Consumable);
        builder.AddProduct(sackOfGems, ProductType.Consumable);
        builder.AddProduct(boxOfGems, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyPileOfGems()
    {
        BuyProductID(pileOfGems);
    }

    public void BuyJarOfGems()
    {
        BuyProductID(jarOfGems);
    }

    public void BuyBagOfGems()
    {
        BuyProductID(bagOfGems);
    }

    public void BuySackOfGems()
    {
        BuyProductID(sackOfGems);
    }

    public void BuyBoxOfGems()
    {
        BuyProductID(boxOfGems);
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            var apple = m_storeExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
    }

    private bool IsInitialized()
    {
        return m_storeController != null && m_storeExtensionProvider != null;
    }

    private void BuyProductID(string productID)
    {
        if (IsInitialized())
        {
            Product product = m_storeController.products.WithID(productID);

            if (product != null && product.availableToPurchase)
            {
                m_storeController.InitiatePurchase(product);
            }
        }
    }

    // IStoreListener

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_storeController = controller;
        m_storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"OnInitializeFailed InitializationFailureReason: {error}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format($"OnPurchaseFailed: FAIL. Product: '{product.definition.storeSpecificId}', PurchaseFailureReason: {failureReason}"));
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id, pileOfGems, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", purchaseEvent.purchasedProduct.definition.id));
            Player.Instance.AdjustGems(pileGemAmount);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, jarOfGems, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", purchaseEvent.purchasedProduct.definition.id));
            Player.Instance.AdjustGems(jarGemAmount);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, bagOfGems, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", purchaseEvent.purchasedProduct.definition.id));
            Player.Instance.AdjustGems(bagGemAmount);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, sackOfGems, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", purchaseEvent.purchasedProduct.definition.id));
            Player.Instance.AdjustGems(sackGemAmount);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, boxOfGems, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", purchaseEvent.purchasedProduct.definition.id));
            Player.Instance.AdjustGems(boxGemAmount);
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", purchaseEvent.purchasedProduct.definition.id));
        }

        return PurchaseProcessingResult.Complete;
    }
}
