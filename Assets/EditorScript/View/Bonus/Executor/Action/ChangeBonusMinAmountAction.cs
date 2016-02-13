using System;
using UnityEngine;

public class ChangeBonusMinAmountAction : IChangeBonusAction
{
    int minAmount;

    public ChangeBonusMinAmountAction(string minAmount) {
        try {
            this.minAmount = Convert.ToInt16(minAmount);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(BonusModelComponent component) {
        component.minAmount = minAmount;
    }
}