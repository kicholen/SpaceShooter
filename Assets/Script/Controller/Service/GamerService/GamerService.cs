public class GamerService : IGamerService
{
    GamerModel gamerModel;

    public GamerModel GamerModel { get { return gamerModel; } }

    public GamerService()
    {
        gamerModel = Utils.Deserialize<GamerModel>();
    }

    void Save()
    {
        Utils.Serialize(gamerModel);
    }
}
