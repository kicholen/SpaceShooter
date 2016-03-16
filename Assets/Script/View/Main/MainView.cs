using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class MainView : View, IView
{
    IServices services;

    Dictionary<PanelType, IPanel> panels = new Dictionary<PanelType, IPanel>();
    PanelType activePanelType;

    public MainView(IServices services) : base("View/MainView")
    {
        this.services = services;
    }

    public override void Init()
    {
        base.Init();
        attachPanels();
        addListeners();
        setCurrentPanel(PanelType.PLAY);
    }

    public override void Destroy()
    {
        base.Destroy();
        services.EventService.RemoveListener<BottomButtonClickedEvent>(onBottomButtonClicked);
    }

    public override void OnShown(Entity e = null)
    {
        base.OnShown(e);
        go.AddComponent<MainViewComponent>();
    }

    void attachPanels()
    {
        panels.Add(PanelType.SHOP, new ShopPanel(getChild("ShopPanel")));
        panels.Add(PanelType.SHIP, new ShipPanel(getChild("ShipPanel")));
        panels.Add(PanelType.PLAY, new PlayPanel(getChild("PlayPanel")));
        panels.Add(PanelType.SETTINGS, new SettingsPanel(getChild("SettingsPanel")));
    }

    void addListeners()
    {
        services.EventService.AddListener<BottomButtonClickedEvent>(onBottomButtonClicked);
    }

    void onBottomButtonClicked(BottomButtonClickedEvent e)
    {
        if (activePanelType == e.type)
            return;

        go.GetComponent<MainViewComponent>().SwitchToScreen(e.type);
        setCurrentPanel(e.type);
        activePanelType = e.type;
    }

    void setCurrentPanel(PanelType panelType)
    {
        if (activePanelType != PanelType.NONE)
            panels[activePanelType].Disable();
        panels[panelType].Enable();
    }
}
