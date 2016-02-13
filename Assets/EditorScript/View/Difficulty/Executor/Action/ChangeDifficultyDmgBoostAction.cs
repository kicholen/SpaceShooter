using System;
using UnityEngine;

public class ChangeDifficultyDmgBoostAction : IChangeDifficultyAction
{
    int dmgBoostPercent;

    public ChangeDifficultyDmgBoostAction(string dmgBoostPercent) {
        try {
            this.dmgBoostPercent = Convert.ToInt16(dmgBoostPercent);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(DifficultyModelComponent component) {
        component.dmgBoostPercent = dmgBoostPercent;
    }
}