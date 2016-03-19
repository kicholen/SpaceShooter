using RSG;
using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;

public class ShopService : IShopService
{
    const int shopItems = 6;

    ICurrencyService currencyService;
    EventService eventService;
    IAPService iapService;

    List<ShopModel> gemItems = new List<ShopModel>();
    List<ShopModel> coinItems = new List<ShopModel>();

    Promise<bool> promise;

    public List<ShopModel> GemItems { get { return gemItems; } }
    public List<ShopModel> CoinItems { get { return coinItems; } }

    public ShopService(ICurrencyService currencyService, EventService eventService, IAPService iapService)
    {
        this.currencyService = currencyService;
        this.eventService = eventService;
        this.iapService = iapService;
    }

    public void Init()
    {
        createShopItems();
        addIAPListeners();
    }

    void createShopItems()
    {
        for (int i = 0; i < shopItems; i++)
        {
            ShopModel shopModel = Utils.Deserialize<ShopModel>(i.ToString());
            if (shopModel.type == ShopType.COINS)
                coinItems.Add(shopModel);
            else
            {
                iapService.AddConsumableProduct(shopModel.googleId, GooglePlay.Name);
                gemItems.Add(shopModel);
            }
        }
    }

    void addIAPListeners()
    {
        eventService.AddListener<IAPPurchasedEvent>(onItemPurchased);
        eventService.AddListener<IAPFailedEvent>(onItemPurchaseFail);
    }

    void onItemPurchased(IAPPurchasedEvent e)
    {
        foreach (ShopModel model in gemItems)
            if (e.id == model.googleId)
                registerPurchase(model);
    }

    void onItemPurchaseFail(IAPFailedEvent e)
    {
        if (promise != null)
            promise.Reject(new Exception(e.id));
    }

    public IPromise<bool> Buy(ShopModel model)
    {
        return model.type == ShopType.COINS ? BuyCoins(model) : BuyGems(model);
    }

    IPromise<bool> BuyCoins(ShopModel model)
    {
        Promise<bool> promise = new Promise<bool>();

        if (canBuyCoins(model))
        {
            currencyService.IncreaseCoins(model.count);
            currencyService.DecreaseGems(model.price);
            promise.Resolve(true);
        }
        else
            promise.Reject(new Exception());
        return promise;
    }

    IPromise<bool> BuyGems(ShopModel model)
    {
        promise = new Promise<bool>();

        if (iapService.IsInitialized())
            iapService.BuyConsumableProduct(model.googleId);
        else
            promise.Reject(new Exception());
        return promise;
    }

    void registerPurchase(ShopModel model)
    {
        currencyService.IncreaseGems(model.count);
        if (promise != null)
            promise.Resolve(true);
    }

    bool canBuyCoins(ShopModel model)
    {
        return currencyService.Gems >= model.price;
    }
}
