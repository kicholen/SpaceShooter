using UnityEngine;

public class ViewService : IViewService {

	IViewFactoryService viewFactoryService;

	Canvas canvas;
	GameObject touchBlocker;
	IView currentView;
	IView nextView;
	ViewTypes nextViewType;

	public ViewService(EventService eventService, IUIFactoryService uiFactoryService, IViewFactoryService viewFactoryService) {
		this.viewFactoryService = viewFactoryService;

		canvas = uiFactoryService.CreatePrefab("Canvas").GetComponent<Canvas>();
		touchBlocker = uiFactoryService.CreatePrefab("TouchBlocker");
		touchBlocker.transform.SetParent(uiFactoryService.CreatePrefab("Canvas").transform, false); // new canvas cause unity says fu
		eventService.AddListener<ViewShownEvent>(onViewShown);
		eventService.AddListener<ViewHiddenEvent>(onViewHidden);
	}

	public void SetView(ViewTypes type) {
		nextViewType = type;

		if (currentView != null) {
			touchBlocker.SetActive(true);
			nextView = viewFactoryService.CreateView(nextViewType);
			currentView.Hide();
		}
		else {
			currentView = viewFactoryService.CreateView(nextViewType);
			showCurrentView();
		}
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
