using UnityEngine;

public class ViewService : IViewService {
    const string CANVAS_PREFAB_PATH = "Canvas";
    const string TOUCHBLOCKER_PREFAB_PATH = "TouchBlocker";

    IViewFactory factory;

	Canvas canvas;
	public Canvas Canvas { get { return canvas; } }
	GameObject touchBlocker;
	IView currentView;
	IView nextView;

    TopPanelComponent topPanel;
    BottomPanelComponent bottomPanel;

    public ViewService(EventService eventService, IUIFactoryService uiFactoryService) {
        factory = new ViewFactory();
        createCanvasAndTouchBlocker(uiFactoryService);
        addViewShowHiddenListeners(eventService);
    }

    public void Init(IServices services) {
        factory.Init(services);
    }

    public IView SetView(ViewTypes type) {
		if (currentView != null) {
			touchBlocker.SetActive(true);
			nextView = factory.Create(type);
			currentView.Hide();
            return nextView;
		}
		else {
			currentView = factory.Create(type);
			showCurrentView();
            return currentView;
        }
    }

    public void CreateTopPanel(IServices services)
    {
        topPanel = new TopPanelComponent(services);
    }

    public void CreateBottomPanel(IServices services)
    {
        bottomPanel = new BottomPanelComponent(services);
    }

    void createCanvasAndTouchBlocker(IUIFactoryService uiFactoryService) {
        canvas = uiFactoryService.CreatePrefab(CANVAS_PREFAB_PATH).GetComponent<Canvas>();
        touchBlocker = uiFactoryService.CreatePrefab(TOUCHBLOCKER_PREFAB_PATH);
        touchBlocker.transform.SetParent(uiFactoryService.CreatePrefab(CANVAS_PREFAB_PATH).transform, false); // new canvas cause unity says fu
    }

    void addViewShowHiddenListeners(EventService eventService) {
        eventService.AddListener<ViewShownEvent>(onViewShown);
        eventService.AddListener<ViewHiddenEvent>(onViewHidden);
    }

    void onViewShown(ViewShownEvent e) {
		touchBlocker.SetActive(false);
	}

	void onViewHidden(ViewHiddenEvent e) {
		currentView.Destroy();
		currentView = nextView;
		showCurrentView();
	}

	void showCurrentView() {
		touchBlocker.SetActive(true);
		currentView.SetParent(canvas.transform);
		currentView.Show();
        editElementsVisibility();
    }

    void editElementsVisibility()
    {
        showOrHideTopPanel();
        showOrHideBottomPanel();
    }

    void showOrHideTopPanel()
    {
        if (topPanel == null)
            return;
        if (currentView.TopPanelVisible())
            topPanel.Show();
        else
            topPanel.Hide();
    }

    void showOrHideBottomPanel()
    {
        if (bottomPanel == null)
            return;
        if (currentView.BottomPanelVisible())
            bottomPanel.Show();
        else
            bottomPanel.Hide();
    }
}
