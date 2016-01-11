using System;
using System.Collections.Generic;

public interface ILevelService {
    void LoadLevelIds(Action<List<string>> onLevelIdsLoaded);
    void LoadLevelById(long id, Action<LevelModelComponent> onLevelLoaded);
    void CreateNewLevel(Action<LevelModelComponent> onLevelCreated);
    void UpdateLevel(LevelModelComponent component, Action onLevelUpdated);
    void DeleteLevel(long id, Action onLevelDeleted);
}