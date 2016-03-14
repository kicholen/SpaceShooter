public interface ICurrencyService
{
    int Coins { get; }
    void IncreaseCoins(int count);
    void DecreaseCoins(int count);
    void Init();
}
