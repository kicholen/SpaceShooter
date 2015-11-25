using UnityEngine;

public interface IViewService {
	Canvas Canvas { get; }
	void SetView(ViewTypes type);
}
