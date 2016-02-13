﻿using System;
using System.Collections.Generic;

public interface IBonusService
{
    void LoadBonuses(Action onLoaded);
    void LoadBonusIds(Action<Dictionary<long, string>> onLoaded);
    void LoadBonusById(long id, Action<BonusModelComponent> onLoaded);
    void CreateNewBonus(Action<BonusModelComponent> onCreated);
    void UpdateBonus(BonusModelComponent component, Action onLoaded);
    void DeleteBonus(long id, Action onDeleted);
}
