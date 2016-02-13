using System;
using UnityEngine;

public class ChangeDifficultyHpBoostAction : IChangeDifficultyAction
{
    int hpBoostPercent;

    public ChangeDifficultyHpBoostAction(string hpBoostPercent) {
        try {
            this.hpBoostPercent = Convert.ToInt16(hpBoostPercent);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(DifficultyModelComponent component) {
        component.hpBoostPercent = hpBoostPercent;
    }
}