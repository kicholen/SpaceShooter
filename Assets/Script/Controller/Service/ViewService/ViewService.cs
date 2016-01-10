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
