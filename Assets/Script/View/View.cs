using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class View : BaseGui {
    const float animationDuration = 0.1f;

    RectTransform rectTransform;
	Entity entity;

	protected Pool pool;
	protected EventService eventService;
	protected IUIFactoryService uiFactoryService;
    protected ITranslationService translationService;
	
    string prefabPath;

    public GameObject Go { get { return go; } }

    public virtual bool TopPanelVisible() { return true; }
    public virtual bool BottomPanelVisible() { return true; }

    public View(string prefabPath) {
        this.prefabPath = prefabPath;
    }

    public void SetBaseServices(IUIFactoryService uiFactoryService, EventService eventService, ITranslationService translationService) {
        this.uiFactoryService = uiFactoryService;
        this.eventService = eventService;
        this.translationService = translationService;
    }

    public void SetPool(Pool pool) {
        this.pool = pool;
    }

    public virtual void Init() {
		go = uiFactoryService.CreatePrefab(prefabPath);
		rectTransform = go.GetComponent<RectTransform>();
		entity = pool.CreateEntity()
			.AddGameObject(go, "View", true);
	}
	
	public void SetParent(Transform parent) {
		go.transform.SetParent(parent, false);
        go.transform.SetAsFirstSibling();
    }
	
	public virtual void Show() {
		Vector2 from = new Vector2(rectTransform.localPosition.x - Screen.width, rectTransform.localPosition.y);
		entity.AddTween(false, new List<Tween>());
		entity.tween.AddTween(entity.gameObject, EaseTypes.bounceOut, GameObjectAccessorType.LOCAL_X, animationDuration)
			.From(from.x)
			.To(rectTransform.localPosition.x)
			.SetEndCallback(OnShown);
		rectTransform.localPosition = from;
	}
	
	public virtual void Hide() {
		Vector2 to = new Vector2(rectTransform.localPosition.x + Screen.width, rectTransform.localPosition.y);
		entity.AddTween(false, new List<Tween>());
		entity.tween.AddTween(entity.gameObject, EaseTypes.bounceIn, GameObjectAccessorType.LOCAL_X, animationDuration)
			.From(rectTransform.localPosition.x)
			.To(to.x)
			.SetEndCallback(OnHidden);
	}
	
	public virtual void OnShown(Entity e = null) {
		eventService.Dispatch<ViewShownEvent>(new ViewShownEvent());
	}
	
	public void OnHidden(Entity e = null) {
		eventService.Dispatch<ViewHiddenEvent>(new ViewHiddenEvent());
	}
	
	public override void Destroy() {
		pool.DestroyEntity(entity);
        base.Destroy();
	}
}