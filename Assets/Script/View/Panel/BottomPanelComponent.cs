using Entitas;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanelComponent : BaseGui
{
    IServices services;

    RectTransform rectTransform;
    Entity entity;

    float showYPosition;
    bool isVisible;

    public BottomPanelComponent(IServices services)
    {
        this.services = services;
        init();
        showYPosition = go.transform.localPosition.y;
        go.SetActive(false);
    }

    public void Show()
    {
        if (!isVisible)
        {
            isVisible = true;
            go.SetActive(true);
            entity.AddTween(false, new List<Tween>());
            entity.tween.AddTween(entity.gameObject, EaseTypes.linear, GameObjectAccessorType.LOCAL_Y, Config.TOP_PANEL_ANIMATION_DURATION)
                .From(rectTransform.localPosition.y)
                .To(showYPosition);
        }
    }

    public void Hide()
    {
        if (isVisible)
        {
            isVisible = false;
            entity.AddTween(false, new List<Tween>());
            entity.tween.AddTween(entity.gameObject, EaseTypes.linear, GameObjectAccessorType.LOCAL_Y, Config.TOP_PANEL_ANIMATION_DURATION)
                .From(rectTransform.localPosition.y)
                .To(rectTransform.localPosition.y + Screen.height * (rectTransform.anchorMax.y - rectTransform.anchorMin.y) * 2.0f);
        }
    }

    void init()
    {
        go = services.UIFactoryService.CreatePrefab("View/Elements/BottomPanel");
        go.transform.SetParent(services.ViewService.Canvas.transform, false);
        setReferences();
        addListeners();
        createEntity();
    }

    void createEntity()
    {
        entity = services.Pool.CreateEntity()
            .AddGameObject(go, "BottomPanelComponent", false);
    }

    void setReferences()
    {
        rectTransform = go.GetComponent<RectTransform>();
    }

    void addListeners()
    {
        getChild("ShopButton").GetComponent<Button>().onClick.AddListener(buttonClicked(PanelType.SHOP));
        getChild("ShipButton").GetComponent<Button>().onClick.AddListener(buttonClicked(PanelType.SHIP));
        getChild("PlayButton").GetComponent<Button>().onClick.AddListener(buttonClicked(PanelType.PLAY));
        getChild("SettingsButton").GetComponent<Button>().onClick.AddListener(buttonClicked(PanelType.SETTINGS));
    }

    UnityEngine.Events.UnityAction buttonClicked(PanelType type)
    {
        return () => services.EventService.Dispatch<BottomButtonClickedEvent>(new BottomButtonClickedEvent(type));
    }
}
