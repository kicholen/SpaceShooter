using UnityEngine.UI;
using Entitas;

public class LoadView : View, IView {

	Scrollbar progressBar;

	public LoadView(Pool pool, IUIFactoryService uiFactoryService, EventService eventService)
	: base(pool, uiFactoryService, eventService, "View/LoadView") {
		progressBar = go.transform.FindChild("LoadingBar").GetComponent<Scrollbar>();
		
		eventService.AddListener<LoadProgressEvent>(onLoadProgress);
	}
	
	public override void Destroy() {
		base.Destroy();
		eventService.RemoveListener<LoadProgressEvent>(onLoadProgress);
	}

	void onLoadProgress(LoadProgressEvent e) {
		progressBar.size = e.progress;
	}
}