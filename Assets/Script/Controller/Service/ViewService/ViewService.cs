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
			currentView.SetParent(canvas.transform);
			currentView.Show();
		}
	}

	void onViewShown(ViewShownEvent e) {
		// unblock View safety?
	}

	void onViewHidden(ViewHiddenEvent e) {
		currentView.Destroy();

		currentView = nextView;
		currentView.SetParent(canvas.transform);
		currentView.Show();
	}
}
