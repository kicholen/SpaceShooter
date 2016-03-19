using RSG;
using System.Collections.Generic;

public interface IShopService
{
    List<ShopModel> GemItems { get; }
    List<ShopModel> CoinItems { get; }

    void Init();
    IPromise<bool> Buy(ShopModel model);
}
