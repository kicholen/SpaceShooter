using UnityEngine;
using Entitas;

public class View {
	const float pixelsPerUnit = 100.0f;

	RectTransform rectTransform;

	protected Pool pool;
	protected EventService eventService;
	protected IUIFactoryService uiFactoryService;
	
	protected GameObject go;

	public GameObject Go { get { return go; } }

	public View(Pool pool, IUIFactoryService uiFactoryService, EventService eventService, string prefab) {
		this.pool = pool;
		this.eventService = eventService;
		this.uiFactoryService = uiFactoryService;
		go = uiFactoryService.CreatePrefab(prefab);
		rectTransform = go.GetComponent<RectTransform>();
	}
	
	public void SetParent(Transform parent) {
		go.transform.SetParent(parent, false);
	}
	
	public virtual void Show() {
		float from = rectTransform.localPosition.x - Screen.width;
		pool.CreateEntity()
			.AddTween(0.0f, 0.1f, EaseTypes.Linear, from, rectTransform.localPosition.x, 0.0f, false, onUpdate, OnShown);
		onUpdate(null, from);
	}
	
	void onUpdate(Entity e, float x) {
		rectTransform.localPosition = new Vector3(x, go.transform.localPosition.y);
	}
	
	public virtual void Hide() {
		pool.CreateEntity()
			.AddTween(0.0f, 0.1f, EaseTypes.Linear, rectTransform.localPosition.x, rectTransform.localPosition.x + Screen.width, 0.0f, false, onUpdate, OnHidden);
	}
	
	public void OnShown(Entity e) {
		if (e != null) {
			e.isDestroyEntity = true;
		}
		eventService.Dispatch<ViewShownEvent>(new ViewShownEvent());
	}
	
	public void OnHidden(Entity e) {
		if (e != null) {
			e.isDestroyEntity = true;
		}
		eventService.Dispatch<ViewHiddenEvent>(new ViewHiddenEvent());
	}
	
	public virtual void Destroy() {
		Object.Destroy(go);
	}
}