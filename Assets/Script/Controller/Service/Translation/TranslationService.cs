using System.Collections.Generic;
using UnityEngine;

public class TranslationService : ITranslationService
{
    const string DEFAULT_LANGUAGE = "eng";

    ISettingsService settingsService;

    Dictionary<SystemLanguage, string> languages;
    Dictionary<string, LanguageModel> translation = new Dictionary<string, LanguageModel>();
    string language;

    public TranslationService(ISettingsService settingsService) {
        this.settingsService = settingsService;
        createAvailableLanguagesMap();
    }

    public void Init() {
        language = getLanguage();
        LanguageModel model = Utils.Deserialize<LanguageModel>(language);
        model.Init();
        translation.Add(language, model);
    }

    public string Translate(string value) {
        string result = value;
        translation[language].data.TryGetValue(value, out result);
        return result;
    }

    string getLanguage() {
        string result;
        languages.TryGetValue(Application.systemLanguage, out result);
        return result != null ? result : DEFAULT_LANGUAGE;
    }

    void createAvailableLanguagesMap() {
        languages = new Dictionary<SystemLanguage, string>();
        languages.Add(SystemLanguage.English, "eng");
    }
}