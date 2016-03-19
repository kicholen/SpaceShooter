public class ExpChangedEvent : GameEvent
{
    public long exp;

    public ExpChangedEvent(long exp)
    {
        this.exp = exp;
    }
}