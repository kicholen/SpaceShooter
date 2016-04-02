using RSG;
using System.Collections.Generic;

public interface IShopService
{
    List<ShopModel> GemItems { get; }
    List<ShopModel> CoinItems { get; }

    void Init();
    IPromise Buy(ShopModel model);
}
