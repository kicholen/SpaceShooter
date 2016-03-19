public class IAPFailedEvent : GameEvent
{
    public string id;

    public IAPFailedEvent(string id)
    {
        this.id = id;
    }
}