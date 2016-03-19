using Entitas;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TopPanelComponent : BaseGui
{
    const float hidePositionOffset = 20.0f;

    IServices services;

    RectTransform rectTransform;
    Entity entity;

    Button addCoinsButton;
    Text coinsText;
    Button addGemsButton;
    Text gemsText;
    Button levelButton;
    Text levelText;
    Slider levelSlider;

    bool isVisible;
    float showYPosition;

    public TopPanelComponent(IServices services)
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
                .To(rectTransform.localPosition.y + Screen.height * (rectTransform.anchorMax.y - rectTransform.anchorMin.y) * 2.0f + hidePositionOffset);
        }
    }

    void init()
    {
        go = services.UIFactoryService.CreatePrefab("View/Elements/TopPanel");
        go.transform.SetParent(services.ViewService.Canvas.transform, false);
        setReferences();
        setData();
        addListeners();
        createEntity();
    }

    void createEntity()
    {
        entity = services.Pool.CreateEntity()
            .AddGameObject(go, "TopPanelComponent", false);
    }

    void setReferences()
    {
        rectTransform = go.GetComponent<RectTransform>();
        addCoinsButton = getChild("CoinsButton/AddButton").GetComponent<Button>();
        coinsText = getChild("CoinsButton/Text").GetComponent<Text>();
        addGemsButton = getChild("GemsButton/AddButton").GetComponent<Button>();
        gemsText = getChild("GemsButton/Text").GetComponent<Text>();
        levelButton = getChild("LevelButton").GetComponent<Button>();
        levelText = getChild("LevelButton/Slider/Fill Area/Text").GetComponent<Text>();
        levelSlider = getChild("LevelButton/Slider").GetComponent<Slider>();
    }

    void setData()
    {
        coinsText.text = services.CurrencyService.Coins.ToString();
        gemsText.text = services.CurrencyService.Gems.ToString();
        levelText.text = "80% mock";
        levelSlider.value = 0.8f;
    }

    void addListeners()
    {
        services.EventService.AddListener<CoinsChangedEvent>(onCoinsChanged);
        services.EventService.AddListener<GemsChangedEvent>(onGemsChanged);
    }

    void onCoinsChanged(CoinsChangedEvent e)
    {
        coinsText.text = e.coins.ToString();
    }

    void onGemsChanged(GemsChangedEvent e)
    {
        gemsText.text = e.gems.ToString();
    }

    void deactivateButtons()
    {
        addCoinsButton.interactable = false;
        addGemsButton.interactable = false;
        levelButton.interactable = false;
    }

    void activateButtons()
    {
        addCoinsButton.interactable = true;
        addGemsButton.interactable = true;
        levelButton.interactable = true;
    }
}
