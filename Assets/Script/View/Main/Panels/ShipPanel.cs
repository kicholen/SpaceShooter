using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipPanel : BaseGui, IPanel
{
    public PanelType PanelType { get { return PanelType.SHIP; } }

    IServices services;
    Transform shipContent;
    Transform infoContent;

    Entity textEntity;

    public ShipPanel(Transform content, IServices services)
    {
        go = content.gameObject;
        this.services = services;
        shipContent = getChild("Viewport/Content");
        infoContent = getChild("Panel/Text");
        setData();
    }

    public void Disable()
    {
    }

    public void Enable()
    {
    }

    public override void Destroy()
    {
        services.Pool.DestroyEntity(textEntity);
        base.Destroy();
    }

    void setData()
    {
        createTextEntity();
        tweenInfoTextIfCan();
        createUpgrades();
    }

    void createTextEntity()
    {
        textEntity = services.Pool.CreateEntity()
            .AddGameObject(infoContent.gameObject, "", false)
            .AddTween(false, new List<Tween>());
    }

    void tweenInfoTextIfCan()
    {
        if (services.ShipService.IsUpgradeInProgress())
        {
            long timeLeft = services.ShipService.UpgradeTimeLeft();
            textEntity.tween.Clear();
            textEntity.tween.AddTween(textEntity.gameObject, EaseTypes.linear, GameObjectAccessorType.TEXT, timeLeft)
                .From(timeLeft)
                .To(0.0f)
                .BlockClear();
        }
    }

    void createUpgrades()
    {
        foreach (KeyValuePair<UpgradeType, ShipUpgrade> entry in services.ShipService.Upgrades)
        {
            GameObject element = services.UIFactoryService.CreatePrefab("View/Elements/ShipUpgradeElement");
            element.name = entry.Key.ToString();
            element.transform.SetParent(shipContent, false);
            services.UIFactoryService.AddText(element.transform, "Text", entry.Value.description);
            services.UIFactoryService.AddButton(element.transform, "UpgradeButton", onPlusClicked);
        }
    }

    void onPlusClicked()
    {
        UpgradeType type = (UpgradeType)Enum.Parse(typeof(UpgradeType), EventSystem.current.currentSelectedGameObject.transform.parent.name);
        services.ShipService.UpgradeIfCan(type)
            .Catch((exception) => Debug.Log("can't upgrade"))
            .Done(tweenInfoTextIfCan);
    }
}
