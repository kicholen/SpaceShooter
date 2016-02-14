using Entitas;

public class SettingsService : ISettingsService
{
    Pool pool;
    Entity entity;

    public bool Music { get { return entity.settingsModel.music; } }
    public bool Sound { get { return entity.settingsModel.sound; } }
    public int Difficulty { get { return entity.settingsModel.difficulty; } }
    public string Language { get { return entity.settingsModel.language; } }

    public SettingsService(Pool pool) {
        this.pool = pool;
    }

    public void Init() {
        entity = pool.GetGroup(Matcher.SettingsModel).GetSingleEntity();
    }

    public void Save() {
        Utils.Serialize(entity.settingsModel);
    }

    public void SetDifficulty(int difficulty) {
        SettingsModelComponent model = entity.settingsModel;
        entity.ReplaceSettingsModel(difficulty, model.music, model.sound, model.language);
    }

    public void SetLanguage(string language) {
        SettingsModelComponent model = entity.settingsModel;
        entity.ReplaceSettingsModel(model.difficulty, model.music, model.sound, language);
    }

    public void SetMusic(bool music) {
        SettingsModelComponent model = entity.settingsModel;
        entity.ReplaceSettingsModel(model.difficulty, music, model.sound, model.language);
    }

    public void SetSound(bool sound) {
        SettingsModelComponent model = entity.settingsModel;
        entity.ReplaceSettingsModel(model.difficulty, model.music, sound, model.language);
    }
}
