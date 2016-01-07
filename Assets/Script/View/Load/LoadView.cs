using UnityEngine.UI;
using Entitas;

public class LoadView : View, IView {

	Scrollbar progressBar;

	public LoadView(Pool pool, IUIFactoryService uiFactoryService, EventService eventService)
	: base(pool, uiFactoryService, eventService, "View/LoadView") {
		progressBar = go.transform.FindChild("LoadingBar").GetComponent<Scrollbar>();
		
		eventService.AddListener<PhaseProgressEvent>(onLoadProgress);
	}
	
	public override void Destroy() {
		base.Destroy();
		eventService.RemoveListener<PhaseProgressEvent>(onLoadProgress);
	}

	void onLoadProgress(PhaseProgressEvent e) {
		progressBar.size = e.progress;
	}
}