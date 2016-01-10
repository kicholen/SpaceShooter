using UnityEngine.UI;
using Entitas;

public class LoadView : View, IView {

	Scrollbar progressBar;

	public LoadView() : base("View/LoadView") { }

    public override void Init() {
        base.Init();
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