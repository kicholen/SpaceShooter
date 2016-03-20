public interface IGamerService
{
    GamerModel Model { get; }
    ProgressModel ProgressModel { get; }
    int Level { get; }
    void Init();
    string GetTopPanelFormattedText();
    float GetNextLevelRatio();
    bool WillLevelUp(long exp);
    void IncreaseExp(long exp);
}