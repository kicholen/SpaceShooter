public interface IGamerService
{
    GamerModel Model { get; }
    int Level { get; }
    long NextLevelExp { get; }
    void Init();
    string GetTopPanelFormattedText();
}