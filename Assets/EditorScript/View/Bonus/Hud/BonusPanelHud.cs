using System;
using System.Collections.Generic;
using UnityEngine;

public class BonusPanelHud : EditorViewUpdaterBase
{
    Transform content;
    BonusModelComponent component;

    BonusActionExecutor executor;

    public BonusPanelHud(Transform content, BonusModelComponent component) {
        this.content = content;
        this.component = component;
        executor = new BonusActionExecutor(component);
        setData();
    }

    void setData() {
        createChangeMinAmountField().transform.SetParent(content, false);
        createChangeMaxAmountField().transform.SetParent(content, false);
        createChangeProbabilityField().transform.SetParent(content, false);
        createChangeBonusResource().transform.SetParent(content, false);
    }

    GameObject createChangeMinAmountField() {
        return createInputElement("minAmount", component.minAmount.ToString(), (value) => {
            executor.Execute(new ChangeBonusMinAmountAction(value));
        });
    }

    GameObject createChangeMaxAmountField() {
        return createInputElement("maxAmount", component.maxAmount.ToString(), (value) => {
            executor.Execute(new ChangeBonusMaxAmountAction(value));
        });
    }

    GameObject createChangeProbabilityField() {
        return createInputElement("probability", component.probability.ToString(), (value) => {
            executor.Execute(new ChangeBonusProbabilityAction(value));
        });
    }

    GameObject createChangeBonusResource() {
        List<string> types = new List<string>() { ResourceWithColliders.Star, ResourceWithColliders.Bonus };
        return createDropdownElementOfString("resource", component.resource, types, (value) => {
            executor.Execute(new ChangeBonusResourceAction(types[Convert.ToInt16(value)]));
        });
    }
}