public class CurrenyService : ICurrencyService
{
    EventService eventService;

    int coins;
    int gems;
    IGamerService gamerService;

    public int Coins { get { return coins; } }
    public int Gems { get { return gems; } }

    public CurrenyService(EventService eventService, IGamerService gamerService)
    {
        this.eventService = eventService;
        this.gamerService = gamerService;
    }

    public void Init()
    {
        coins = gamerService.GamerModel.coins;
        gems = gamerService.GamerModel.gems;
    }

    public bool CanBePurchased(int price)
    {
        return coins >= price;
    }

    public void DecreaseCoins(int count)
    {
        coins -= count;
        eventService.Dispatch<CoinsChangedEvent>(new CoinsChangedEvent(coins));
    }

    public void IncreaseCoins(int count)
    {
        coins += count;
        eventService.Dispatch<CoinsChangedEvent>(new CoinsChangedEvent(coins));
    }

    public void DecreaseGems(int count)
    {
        gems -= count;
        eventService.Dispatch<GemsChangedEvent>(new GemsChangedEvent(gems));
    }

    public void IncreaseGems(int count)
    {
        gems += count;
        eventService.Dispatch<GemsChangedEvent>(new GemsChangedEvent(gems));
    }
}
