using UnityEngine.UI;
using Entitas;

public class InitView : View, IView {

	Scrollbar progressBar;

	public InitView(Pool pool, IUIFactoryService uiFactoryService, EventService eventService)
	: base(pool, uiFactoryService, eventService, "View/InitView") {
		progressBar = go.transform.FindChild("LoadingBar").GetComponent<Scrollbar>();

		eventService.AddListener<LoadProgressEvent>(onLoadProgress);
	}

	public override void Show() {
		OnShown();
	}

	public override void Destroy() {
		base.Destroy();
		eventService.RemoveListener<LoadProgressEvent>(onLoadProgress);
	}
	
	void onLoadProgress(LoadProgressEvent e) {
		progressBar.size = e.progress;
	}
}