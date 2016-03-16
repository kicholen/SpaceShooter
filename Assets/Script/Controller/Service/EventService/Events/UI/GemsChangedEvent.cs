public class GemsChangedEvent : GameEvent
{
    public int gems;

    public GemsChangedEvent(int gems)
    {
        this.gems = gems;
    }
}