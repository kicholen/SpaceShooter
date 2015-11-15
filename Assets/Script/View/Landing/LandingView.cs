using UnityEngine;

public class LandingView : IView {
	
	EventService eventService;
	ILoadService loadService;
	GameObject go;
	
	public GameObject Go { get { return go; } }
	
	public LandingView(IUIFactoryService uiFactoryService, EventService eventService, ILoadService loadService) {
		this.eventService = eventService;
		this.loadService = loadService;
		go = uiFactoryService.CreatePrefab("View/LandingView");
		uiFactoryService.AddButton(go.transform, "PlayButton", onPlayClicked);
	}
	
	public void SetParent(Transform parent) {
		go.transform.SetParent(parent, false);
	}
	
	public void Show() {
		OnShown();
	}
	
	public void Hide() {
		OnHidden();
	}
	
	public void OnShown() {
		
		eventService.Dispatch<ViewShownEvent>(new ViewShownEvent());
	}
	
	public void OnHidden() {
		
		eventService.Dispatch<ViewHiddenEvent>(new ViewHiddenEvent());
	}
	
	public void Destroy() {
		Object.Destroy(go);
	}

	void onPlayClicked() {
		loadService.ExecutePlayGame(1);
	}
}