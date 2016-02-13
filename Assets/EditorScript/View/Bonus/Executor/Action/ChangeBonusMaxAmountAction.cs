using System;
using UnityEngine;

public class ChangeBonusMaxAmountAction : IChangeBonusAction
{
    int maxAmount;

    public ChangeBonusMaxAmountAction(string maxAmount) {
        try {
            this.maxAmount = Convert.ToInt16(maxAmount);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(BonusModelComponent component) {
        component.maxAmount = maxAmount;
    }
}