using RSG;
using System;
using System.Collections.Generic;

public class ShopService : IShopService
{
    const int shopItems = 6;

    ICurrencyService currencyService;

    List<ShopModel> gemItems = new List<ShopModel>();
    List<ShopModel> coinItems = new List<ShopModel>();

    public List<ShopModel> GemItems { get { return gemItems; } }
    public List<ShopModel> CoinItems { get { return coinItems; } }

    public ShopService(ICurrencyService currencyService)
    {
        this.currencyService = currencyService;
    }

    public void Init()
    {
        for (int i = 0; i < shopItems; i++)
        {
            ShopModel shopModel = Utils.Deserialize<ShopModel>(i.ToString());
            if (shopModel.type == ShopType.COINS)
                coinItems.Add(shopModel);
            else
                gemItems.Add(shopModel);
        }
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
        Promise<bool> promise = new Promise<bool>();

        if (true)
        {
            currencyService.IncreaseGems(model.count);
            promise.Resolve(true);
        }
        else
            promise.Reject(new Exception());
        return promise;
    }

    bool canBuyCoins(ShopModel model)
    {
        return currencyService.Gems >= model.price;
    }
}
