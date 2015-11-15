using UnityEngine;

public class LoadView : IView {
	
	EventService eventService;
	GameObject go;
	
	public GameObject Go { get { return go; } }
	
	public LoadView(IUIFactoryService uiFactoryService, EventService eventService) {
		this.eventService = eventService;
		go = uiFactoryService.CreatePrefab("View/LoadView");
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
}