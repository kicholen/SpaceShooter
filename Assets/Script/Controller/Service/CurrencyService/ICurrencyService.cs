public interface ICurrencyService
{
    int Coins { get; }
    int Gems { get; }
    bool CanBePurchased(int cost);
    void IncreaseCoins(int count);
    void DecreaseCoins(int count);
    void IncreaseGems(int count);
    void DecreaseGems(int count);
    void Init();
}
