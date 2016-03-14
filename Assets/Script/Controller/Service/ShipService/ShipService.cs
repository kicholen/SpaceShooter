using Entitas;

public class ShipService : IShipService
{
    Pool pool;
    Group group;

    public void Init(IServices services)
    {
        pool = services.Pool;
        group = pool.GetGroup(Matcher.ShipModel);
        setCurrentShip();
    }

    public void UpgradeHealth()
    {
        
    }

    public void UpgradeLaser()
    {

    }

    public void UpgradeMissiles()
    {

    }

    public void UpgradeSecondaryMissiles()
    {

    }

    void setCurrentShip()
    {
        pool.CreateEntity()
            .AddCurrentShip(group.GetEntities()[0].shipModel);
    }
}