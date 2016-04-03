using Entitas;
using RSG;
using System.Collections.Generic;

public interface IShipService
{
    void Init(Pool pool, IGamerService gamerService, ICurrencyService currencyService);
    bool IsUpgradeInProgress();
    long UpgradeTimeLeft();
    Dictionary<UpgradeType, ShipUpgrade> Upgrades { get; }
    IPromise UpgradeIfCan(UpgradeType type);
}