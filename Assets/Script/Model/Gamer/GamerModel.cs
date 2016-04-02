using System.Collections.Generic;

public class GamerModel
{
    public string name;
    public int coins;
    public int gems;
    public long experience;
    public Dictionary<UpgradeType, int> upgrades;
    public UpgradeType upgradeInProgress;
    public long upgradeStartTime;
}
