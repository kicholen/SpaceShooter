using Entitas;
using RSG;
using System.Collections.Generic;

public class ShipService : IShipService
{
    const int upgradeTypes = 10;

    Pool pool;
    Group group;
    IGamerService gamerService;
    ICurrencyService currencyService;
    ITimeService timeService;
    EventService eventService;

    Dictionary<UpgradeType, ShipUpgrade> upgrades = new Dictionary<UpgradeType, ShipUpgrade>();

    public ShipService(ITimeService timeService, EventService eventService)
    {
        this.timeService = timeService;
        this.eventService = eventService;
    }

    public void Init(Pool pool, IGamerService gamerService, ICurrencyService currencyService)
    {
        this.pool = pool;
        this.gamerService = gamerService;
        this.currencyService = currencyService;
        group = pool.GetGroup(Matcher.ShipModel);
        prepare();
        if (isUpgradeInProgress())
            registerUpgradeCall(gamerService.Model.upgradeInProgress, gamerService.Model.upgrades[gamerService.Model.upgradeInProgress]);
    }

    void prepare()
    {
        loadShipUgrades();
        updateModel();
        setCurrentShip();
    }

    public IPromise UpgradeIfCan(UpgradeType type)
    {
        Promise promise = new Promise();
        if (isUpgradeInProgress())
            promise.Reject(new UpgradeInProgressException());
        if (IsMaxUpgrade(type))
            promise.Reject(new MaxUpgradeException());
        else if (!currencyService.CanBePurchased(getNexUpgradeCost(type)))
            promise.Reject(new NotEnoughCoinsException());
        else
        {
            startUpgrade(type);
            promise.Resolve();
        }
        return promise;
    }

    void startUpgrade(UpgradeType type)
    {
        int level = gamerService.Model.upgrades[type];
        currencyService.DecreaseCoins(upgrades[type].cost[level]);
        gamerService.Model.upgradeInProgress = type;
        gamerService.Model.upgradeStartTime = timeService.Now;
        registerUpgradeCall(type, level);
    }

    void loadShipUgrades()
    {
        for (int i = 0; i < upgradeTypes; i++)
        {
            ShipUpgrade upgradeModel = Utils.Deserialize<ShipUpgrade>(i.ToString());
            upgrades.Add(upgradeModel.type, upgradeModel);
        }
    }

    void updateModel()
    {
        ShipModelComponent model = group.GetEntities()[0].shipModel;
        foreach (KeyValuePair<UpgradeType, int> entry in gamerService.Model.upgrades)
            updateModelByUpgrade(model, entry.Key, entry.Value);
    }

    void updateModelByUpgrade(ShipModelComponent model, UpgradeType key, int value)
    {
        while (value > 0)
        {
            float bonus = upgrades[key].bonus[value - 1];
            applyUpgrade(model, key, bonus);
            --value;
        }
    }

    void applyUpgrade(ShipModelComponent model, UpgradeType key, float bonus)
    {
        switch (key)
        {
            case UpgradeType.Health:
                model.health += (int)bonus;
                break;
            case UpgradeType.LaserDamage:
                model.hasLaser = true;
                model.laserDamage += (int)bonus;
                break;
            case UpgradeType.MagnetRadius:
                model.hasMagnetField = true;
                model.magnetRadius += bonus;
                break;
            case UpgradeType.MissileDamage:
                model.missileDamage += (int)bonus;
                break;
            case UpgradeType.MissileSpawnDelay:
                model.missileSpawnDelay += bonus;
                break;
            case UpgradeType.MissileSpeed:
                model.missileVelocity += bonus;
                break;
            case UpgradeType.SecondaryMisileSpawnDelay:
                model.hasSecondaryMissiles = true;
                model.secondaryMissileSpawnDelay += bonus;
                break;
            case UpgradeType.SecondaryMissileDamage:
                model.hasSecondaryMissiles = true;
                model.secondaryMissileDamage += (int)bonus;
                break;
            case UpgradeType.SecondaryMissileSpeed:
                model.hasSecondaryMissiles = true;
                model.secondaryMissileVelocity += bonus;
                break;
            case UpgradeType.Speed:
                model.maxVelocity += bonus;
                break;
        }
    }

    void setCurrentShip()
    {
        pool.CreateEntity()
            .AddCurrentShip(group.GetEntities()[0].shipModel);
    }

    bool isUpgradeInProgress()
    {
        return gamerService.Model.upgradeInProgress != UpgradeType.None;
    }

    void onUpgradeFinished()
    {
        UpgradeType type = gamerService.Model.upgradeInProgress;
        int value = gamerService.Model.upgrades[type] + 1;
        gamerService.Model.upgrades[type] = value;
        applyUpgrade(group.GetEntities()[0].shipModel, type, upgrades[type].bonus[value - 1]);
    }

    int getNexUpgradeCost(UpgradeType type)
    {
        return upgrades[type].cost[gamerService.Model.upgrades[type]];
    }

    bool IsMaxUpgrade(UpgradeType type)
    {
        return gamerService.Model.upgrades[type] == upgrades[type].cost.Count;
    }

    void registerUpgradeCall(UpgradeType type, int level)
    {
        timeService.RegisterCall(onUpgradeFinished, gamerService.Model.upgradeStartTime + (long)upgrades[type].duration[level]);
    }
}