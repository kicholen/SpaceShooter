using System;
using UnityEngine;

public class ChangeBonusProbabilityAction : IChangeBonusAction
{
    float probability;

    public ChangeBonusProbabilityAction(string probability) {
        try {
            this.probability = (float)Convert.ToDouble(probability);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(BonusModelComponent component) {
        component.probability = probability;
    }
}