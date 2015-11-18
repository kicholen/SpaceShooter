using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Entitas;

public class LevelView : View, IView {
	
	ILoadService loadService;

	Transform content;
	List<int> levels = new List<int>();
	
	public LevelView(Pool pool, IUIFactoryService uiFactoryService, EventService eventService, ILoadService loadService) 
	: base(pool, uiFactoryService, eventService, "View/LevelView") {
		this.loadService = loadService;
		content = go.transform.FindChild("Viewport/Content");

		createLevels();
	}

	void createLevels() {
		levels.Add(1);
		levels.Add(101);
		levels.Add(999);

		for (int i = 0; i < levels.Count; i++) {
			GameObject button = uiFactoryService.CreatePrefab("Element/SimpleButton");
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