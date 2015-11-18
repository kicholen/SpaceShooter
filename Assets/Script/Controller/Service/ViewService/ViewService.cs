using UnityEngine;

public class ViewService : IViewService {

	IViewFactoryService viewFactoryService;

	Canvas canvas;
	IView currentView;
	IView nextView;
	ViewTypes nextViewType;

	public ViewService(EventService eventService, IUIFactoryService uiFactoryService, IViewFactoryService viewFactoryService) {
		this.viewFactoryService = viewFactoryService;

		canvas = uiFactoryService.CreatePrefab("Canvas").GetComponent<Canvas>();
		eventService.AddListener<ViewShownEvent>(onViewShown);
		eventService.AddListener<ViewHiddenEvent>(onViewHidden);
	}

	public void SetView(ViewTypes type) {
		nextViewType = type;

		if (currentView != null) {
			nextView = viewFactoryService.CreateView(nextViewType);
			currentView.Hide();
		}
		else {
			currentView = viewFactoryService.CreateView(nextViewType);
			showCurrentView();
		}
	}

	void onViewShown(ViewShownEvent e) {
	}

	void onViewHidden(ViewHiddenEvent e) {
		currentView.Destroy();

		currentView = nextView;
		showCurrentView();
	}

	void showCurrentView() {
		currentView.SetParent(canvas.transform);
		currentView.Show();
	}
}
