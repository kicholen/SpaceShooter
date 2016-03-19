public class IAPPurchasedEvent : GameEvent
{
    public string id;

    public IAPPurchasedEvent(string id)
    {
        this.id = id;
    }
}