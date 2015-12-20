using UnityEngine;
using Entitas;

public class View {
	const float pixelsPerUnit = 100.0f;

	RectTransform rectTransform;
	Entity entity;

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
		entity = pool.CreateEntity()
				.AddGameObject(go, "prefab")
				.AddPosition(rectTransform.localPosition);
	}
	
	public void SetParent(Transform parent) {
		go.transform.SetParent(parent, false);
	}
	
	public virtual void Show() {
		Vector2 from = new Vector2(rectTransform.localPosition.x - Screen.width, rectTransform.localPosition.y);
		entity.AddTween(false, new System.Collections.Generic.Dictionary<System.Type, Tween>());
		TweenComponent component = (TweenComponent)entity.GetComponent(ComponentIds.Tween);
		component.AddTween(entity.position, EaseTypes.Linear, 3, 0.1f, new float[] {from.x, from.y}, new float[] {rectTransform.localPosition.x, rectTransform.localPosition.y}); 
		//entity.AddTweenPosition(0.0f, 0.1f, EaseTypes.Linear, from, rectTransform.localPosition, false, OnShown, onUpdate);
		rectTransform.localPosition = from;
	}
	
	void onUpdate(Entity e) {
		rectTransform.localPosition = new Vector3(e.position.pos.x, e.position.pos.y);
	}
	
	public virtual void Hide() {
		Vector2 to = new Vector2(rectTransform.localPosition.x + Screen.width, rectTransform.localPosition.y);
		entity.AddTween(false, new System.Collections.Generic.Dictionary<System.Type, Tween>());
		TweenComponent component = (TweenComponent)entity.GetComponent(ComponentIds.Tween);
		component.AddTween(entity.position, EaseTypes.Linear, 3, 0.1f, new float[] {rectTransform.localPosition.x, rectTransform.localPosition.y}, new float[] {to.x, to.y}); 
		//entity.AddTweenPosition(0.0f, 0.1f, EaseTypes.Linear, rectTransform.localPosition, to, false, OnHidden, onUpdate);
	}
	
	public void OnShown(Entity e = null) {
		eventService.Dispatch<ViewShownEvent>(new ViewShownEvent());
	}
	
	public void OnHidden(Entity e = null) {
		eventService.Dispatch<ViewHiddenEvent>(new ViewHiddenEvent());
	}
	
	public virtual void Destroy() {
		entity.isDestroyEntity = true;
		Object.Destroy(go);
	}
}