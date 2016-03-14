using Entitas;
using UnityEngine;

public interface IView {
	GameObject Go { get; }
    bool TopPanelVisible();

    void SetBaseServices(IUIFactoryService uiFactoryService, EventService eventService, ITranslationService translationService);
    void SetPool(Pool pool);
    void Init();
	void SetParent(Transform parent);
	void Destroy();
	void Hide();
	void Show();
}