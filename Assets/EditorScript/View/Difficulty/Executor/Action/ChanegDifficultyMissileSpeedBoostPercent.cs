using System;
using UnityEngine;

public class ChangeDifficultyMissileSpeedBoostPercent : IChangeDifficultyAction
{
    int missileSpeedBoostPercent;

    public ChangeDifficultyMissileSpeedBoostPercent(string missileSpeedBoostPercent) {
        try {
            this.missileSpeedBoostPercent = Convert.ToInt16(missileSpeedBoostPercent);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(DifficultyModelComponent component) {
        component.missileSpeedBoostPercent = missileSpeedBoostPercent;
    }
}