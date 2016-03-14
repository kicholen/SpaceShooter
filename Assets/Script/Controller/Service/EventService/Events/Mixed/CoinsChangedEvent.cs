public class CoinsChangedEvent : GameEvent
{
    public int coins;

    public CoinsChangedEvent(int coins)
    {
        this.coins = coins;
    }
}