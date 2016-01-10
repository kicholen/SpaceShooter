using UnityEngine;

public interface IViewService {
	Canvas Canvas { get; }
    void Init(IServices services);
	void SetView(ViewTypes type);
}
