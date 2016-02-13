using System;
using System.Collections.Generic;

public interface IDifficultyService
{
    void LoadDifficulties(Action onLoaded);
    void LoadDifficultyIds(Action<Dictionary<long, string>> onLoaded);
    void LoadDifficultyById(long id, Action<DifficultyModelComponent> onLoaded);
    void CreateNewDifficulty(Action<DifficultyModelComponent> onCreated);
    void UpdateDifficulty(DifficultyModelComponent component, Action onLoaded);
    void DeleteDifficulty(long id, Action onDeleted);
}