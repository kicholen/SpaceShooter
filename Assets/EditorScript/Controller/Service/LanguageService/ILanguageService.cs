using System;
using System.Collections.Generic;

public interface ILanguageService
{
    void LoadLanguageIds(Action<Dictionary<long, string>> onLanguageIdsLoaded);
    void LoadLanguageById(long id, Action<LanguageModel> onLanguageLoaded);
    void UpdateLanguage(LanguageModel component, Action onLanguageUpdated);
}