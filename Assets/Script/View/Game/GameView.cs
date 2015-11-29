using Entitas;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameView : View, IView, IUpdateable {

	Controller controller;
	IGameService gameService;
	
	public GameView(Controller controller, Pool pool, IUIFactoryService uiFactoryService, IGameService gameService, EventService eventService)
	: base(pool, uiFactoryService, eventService, "View/GameView") {
		this.controller = controller;
		this.gameService = gameService;
		uiFactoryService.AddButton(go.transform, "PauseButton", onPauseClicked);

		eventService.AddListener<GameSlowEvent>(onGameSlow);
		go.SetActive(false);
		
		controller.Services.Updateables.Insert(0, this);
	}

	public void Update() {
		if (go.activeSelf) {
			if (Input.GetMouseButtonDown(0)) {
				PointerEventData cursor = new PointerEventData(EventSystem.current);
				cursor.position = Input.mousePosition;
				List<RaycastResult> objectsHit = new List<RaycastResult>();
				EventSystem.current.RaycastAll(cursor, objectsHit);
				if (objectsHit.Count > 0) {
					objectsHit[0].gameObject.GetComponent<Button>().onClick.Invoke();
				}
			}
		}
	}

	public override void Hide() {
		controller.Services.Updateables.Remove(this);
		eventService.RemoveListener<GameSlowEvent>(onGameSlow);
		OnHidden();
	}

	void onGameSlow(GameSlowEvent e) {
		go.SetActive(e.slow < 1.0f);
	}

	void onPauseClicked() {
		gameService.EndGame(pool.CreateEntity());
	}
}