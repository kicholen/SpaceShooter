using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class LevelView : IView {
	
	EventService eventService;
	IUIFactoryService uiFactoryService;
	ILoadService loadService;

	GameObject go;
	Transform content;
	List<int> levels = new List<int>();

	public GameObject Go { get { return go; } }
	
	public LevelView(IUIFactoryService uiFactoryService, EventService eventService, ILoadService loadService) {
		this.eventService = eventService;
		this.uiFactoryService = uiFactoryService;
		this.loadService = loadService;
		go = uiFactoryService.CreatePrefab("View/LevelView");
		content = go.transform.FindChild("Viewport/Content");

		createLevels();
	}
	
	public void SetParent(Transform parent) {
		go.transform.SetParent(parent, false);
	}
	
	public void Show() {
		OnShown();
	}
	
	public void Hide() {
		OnHidden();
	}
	
	public void OnShown() {
		
		eventService.Dispatch<ViewShownEvent>(new ViewShownEvent());
	}
	
	public void OnHidden() {
		
		eventService.Dispatch<ViewHiddenEvent>(new ViewHiddenEvent());
	}
	
	public void Destroy() {
		Object.Destroy(go);
	}

	void createLevels() {
		levels.Add(1);
		levels.Add(101);
		levels.Add(999);

		for (int i = 0; i < levels.Count; i++) {
			GameObject button = uiFactoryService.CreatePrefab("Button/SimpleButton");
			button.name = levels[i].ToString();
			button.transform.SetParent(content, false);
			uiFactoryService.AddText(button.transform, "Text", button.name);
			uiFactoryService.AddButton(button, onLevelClicked);
		}
	}

	void onLevelClicked() {
		int level = System.Convert.ToInt16(EventSystem.current.currentSelectedGameObject.name);
		loadService.ExecutePlayGame(level);
	}
}