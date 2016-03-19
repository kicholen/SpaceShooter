using Entitas;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanelComponent : BaseGui
{
    const float hidePositionOffset = 20.0f;

    IServices services;

    RectTransform rectTransform;
    Entity entity;

    Dictionary<PanelType, Transform> buttons = new Dictionary<PanelType, Transform>();
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
            activateButtons();
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
            deactivateButtons();
            isVisible = false;
            entity.AddTween(false, new List<Tween>());
            entity.tween.AddTween(entity.gameObject, EaseTypes.linear, GameObjectAccessorType.LOCAL_Y, Config.TOP_PANEL_ANIMATION_DURATION)
                .From(rectTransform.localPosition.y)
                .To(rectTransform.localPosition.y - Screen.height * (rectTransform.anchorMax.y - rectTransform.anchorMin.y) * 2.0f - hidePositionOffset);
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        services.EventService.RemoveListener<PanelSwitchedEvent>(onPanelSwitched);
    }

    void init()
    {
        go = services.UIFactoryService.CreatePrefab("View/Elements/BottomPanel");
        go.transform.SetParent(services.ViewService.Canvas.transform, false);
        setReferences();
        setTranslations();
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
        buttons.Add(PanelType.SHOP, getChild("ShopButton"));
        buttons.Add(PanelType.SHIP, getChild("ShipButton"));
        buttons.Add(PanelType.PLAY, getChild("PlayButton"));
        buttons.Add(PanelType.SETTINGS, getChild("SettingsButton"));
    }

    void setTranslations()
    {
        getChild("ShopButton/Text").GetComponent<Text>().text = services.TranslationService.Translate("SKLEP");
        getChild("ShipButton/Text").GetComponent<Text>().text = services.TranslationService.Translate("STATEK");
        getChild("PlayButton/Text").GetComponent<Text>().text = services.TranslationService.Translate("GRAJ");
        getChild("SettingsButton/Text").GetComponent<Text>().text = services.TranslationService.Translate("USTAWIENIA");
    }

    void addListeners()
    {
        buttons[PanelType.SHOP].GetComponent<Button>().onClick.AddListener(buttonClicked(PanelType.SHOP));
        buttons[PanelType.SHIP].GetComponent<Button>().onClick.AddListener(buttonClicked(PanelType.SHIP));
        buttons[PanelType.PLAY].GetComponent<Button>().onClick.AddListener(buttonClicked(PanelType.PLAY));
        buttons[PanelType.SETTINGS].GetComponent<Button>().onClick.AddListener(buttonClicked(PanelType.SETTINGS));
        services.EventService.AddListener<PanelSwitchedEvent>(onPanelSwitched);
    }

    void onPanelSwitched(PanelSwitchedEvent e)
    {
        foreach (KeyValuePair<PanelType, Transform> entry in buttons)
        {
            if (e.type == entry.Key)
                entry.Value.GetComponent<Image>().color = Color.cyan;
            else
                entry.Value.GetComponent<Image>().color = Color.white;
        }
    }

    UnityEngine.Events.UnityAction buttonClicked(PanelType type)
    {
        return () => services.EventService.Dispatch<BottomButtonClickedEvent>(new BottomButtonClickedEvent(type));
    }

    void deactivateButtons()
    {
        foreach (KeyValuePair<PanelType, Transform> entry in buttons)
            entry.Value.GetComponent<Button>().interactable = false;
    }

    void activateButtons()
    {
        foreach (KeyValuePair<PanelType, Transform> entry in buttons)
            entry.Value.GetComponent<Button>().interactable = true;
    }
}
