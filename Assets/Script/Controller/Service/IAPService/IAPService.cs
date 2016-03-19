using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPService : IStoreListener
{
    EventService eventService;

    ConfigurationBuilder builder;
    IStoreController controller;
    IExtensionProvider extensions;

    public IAPService(EventService eventService)
    {
        this.eventService = eventService;
        builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
    }

    public void Init()
    {
        UnityPurchasing.Initialize(this, builder);
    }

    public bool IsInitialized()
    {
        return controller != null && extensions != null;
    }

    public void AddConsumableProduct(string storeId, string store)
    {
        builder.AddProduct(storeId, ProductType.Consumable, new IDs { { storeId, store} });
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }

    public void BuyConsumableProduct(string id)
    {
        buyProductID(id);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("IAPService:InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, reason));
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        eventService.Dispatch<IAPPurchasedEvent>(new IAPPurchasedEvent(e.purchasedProduct.definition.id));
        return PurchaseProcessingResult.Complete;
    }

    void buyProductID(string productId)
    {
        try
        {
            if (IsInitialized())
            {
                Product product = controller.products.WithID(productId);
                if (product != null && product.availableToPurchase)
                    controller.InitiatePurchase(product);
            }
        }
        catch (Exception e)
        {
            Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
        }
    }
}
