using UnityEngine.UI;

public class InitView : View, IView {

	Scrollbar progressBar;

    public override bool TopPanelVisible() { return false; }

    public InitView() : base("View/InitView") { }

    public override void Init() {
        base.Init();
        progressBar = go.transform.FindChild("LoadingBar").GetComponent<Scrollbar>();
        eventService.AddListener<PhaseProgressEvent>(onLoadProgress);
    }

    public override void Show() {
		OnShown();
	}

	public override void Destroy() {
		base.Destroy();
		eventService.RemoveListener<PhaseProgressEvent>(onLoadProgress);
	}
	
	void onLoadProgress(PhaseProgressEvent e) {
		progressBar.size = e.progress;
	}
}