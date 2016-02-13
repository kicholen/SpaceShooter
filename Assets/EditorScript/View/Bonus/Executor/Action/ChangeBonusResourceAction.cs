using System;
using UnityEngine;

public class ChangeBonusResourceAction : IChangeBonusAction
{
    string resource;

    public ChangeBonusResourceAction(string resource) {
        try {
            this.resource = resource;
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(BonusModelComponent component) {
        component.resource = resource;
    }
}