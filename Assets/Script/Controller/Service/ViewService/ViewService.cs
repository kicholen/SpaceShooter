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
	ViewTypes nextViewType;

	public ViewService(EventService eventService, IUIFactoryService uiFactoryService) {
        factory = new ViewFactory();
        createCanvasAndTouchBlocker(uiFactoryService);
        addViewShowHiddenListeners(eventService);
    }

    public void Init(IServices services) {
        factory.Init(services);
    }

    public void SetView(ViewTypes type) {
		nextViewType = type;

		if (currentView != null) {
			touchBlocker.SetActive(true);
			nextView = factory.Create(nextViewType);
			currentView.Hide();
		}
		else {
			currentView = factory.Create(nextViewType);
			showCurrentView();
		}
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
	}
}
