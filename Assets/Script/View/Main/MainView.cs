using System.Collections.Generic;
using Entitas;

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
        activePanelType = PanelType.PLAY;
        refreshPanelStates();
    }

    public override void Destroy()
    {
        base.Destroy();
        foreach (KeyValuePair<PanelType, IPanel> entry in panels)
            entry.Value.Destroy();
        services.EventService.RemoveListener<BottomButtonClickedEvent>(onBottomButtonClicked);
    }

    public override void OnShown(Entity e = null)
    {
        base.OnShown(e);
        go.AddComponent<MainViewComponent>().PanelSwitched = onPanelSwitched;
    }

    void onPanelSwitched(PanelType panel)
    {
        if (activePanelType == panel)
            return;
        activePanelType = panel;
        refreshPanelStates();
    }

    void attachPanels()
    {
        panels.Add(PanelType.SHOP, new ShopPanel(getChild("Viewport/Content/ShopPanel"), services));
        panels.Add(PanelType.SHIP, new ShipPanel(getChild("Viewport/Content/ShipPanel"), services));
        panels.Add(PanelType.PLAY, new PlayPanel(getChild("Viewport/Content/PlayPanel"), services));
        panels.Add(PanelType.SETTINGS, new SettingsPanel(getChild("Viewport/Content/SettingsPanel"), services));
    }

    void addListeners()
    {
        services.EventService.AddListener<BottomButtonClickedEvent>(onBottomButtonClicked);
    }

    void onBottomButtonClicked(BottomButtonClickedEvent e)
    {
        if (activePanelType == e.type)
            return;

        go.GetComponent<MainViewComponent>().SwitchToPanel(e.type);
    }

    void refreshPanelStates()
    {
        foreach (KeyValuePair<PanelType, IPanel> entry in panels)
        {
            if (activePanelType == entry.Key)
                entry.Value.Enable();
            else
                entry.Value.Disable();
        }
        services.EventService.Dispatch<PanelSwitchedEvent>(new PanelSwitchedEvent(activePanelType));
    }
}
