using Entitas;
using UnityEngine;

public interface IView {
	GameObject Go { get; }
    void SetBaseServices(IUIFactoryService uiFactoryService, EventService eventService);
    void SetPool(Pool pool);
    void Init();
	void SetParent(Transform parent);
	void Destroy();
	void Hide();
	void Show();
}