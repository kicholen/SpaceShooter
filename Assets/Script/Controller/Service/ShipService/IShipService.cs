using Entitas;
using RSG;

public interface IShipService
{
    void Init(Pool pool, IGamerService gamerService, ICurrencyService currencyService);
    IPromise UpgradeIfCan(UpgradeType type);
}