using UnityEngine;
using UnityEngine.UI;

public class LoadView : IView {
	
	EventService eventService;
	GameObject go;
	Scrollbar progressBar;

	public GameObject Go { get { return go; } }
	
	public LoadView(IUIFactoryService uiFactoryService, EventService eventService) {
		this.eventService = eventService;
		go = uiFactoryService.CreatePrefab("View/LoadView");
		progressBar = go.transform.FindChild("LoadingBar").GetComponent<Scrollbar>();
		
		eventService.AddListener<LoadProgressEvent>(onLoadProgress);
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
		eventService.RemoveListener<LoadProgressEvent>(onLoadProgress);
		Object.Destroy(go);
	}

	void onLoadProgress(LoadProgressEvent e) {
		progressBar.size = e.progress;
	}
}