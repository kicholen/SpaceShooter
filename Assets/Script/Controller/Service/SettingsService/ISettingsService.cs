public interface ISettingsService
{
    bool Music { get; }
    bool Sound { get; }
    int Difficulty { get; }
    string Language { get; }

    void Init();
    void Save();
    void SetDifficulty(int difficulty);
    void SetLanguage(string language);
    void SetSound(bool sound);
    void SetMusic(bool music);
}
