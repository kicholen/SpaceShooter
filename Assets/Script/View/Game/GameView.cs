using Entitas;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameView : View, IView, Updateable {

	List<Updateable> updateables; //todo change this one, for example replace with interface
	IGameService gameService;
	
	public GameView(List<Updateable> updateables, IGameService gameService) : base("View/GameView") {
		this.updateables = updateables;
		this.gameService = gameService;
	}

    public override void Init() {
        base.Init();
        uiFactoryService.AddButton(go.transform, "Exit", onExitClicked);
        uiFactoryService.AddButton(go.transform, "Laser", onLaserClicked);

        eventService.AddListener<GameSlowEvent>(onGameSlow);
        go.SetActive(false);

        updateables.Insert(0, this);
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
		updateables.Remove(this);
		eventService.RemoveListener<GameSlowEvent>(onGameSlow);
		OnHidden();
	}

	void onGameSlow(GameSlowEvent e) {
		go.SetActive(e.slow < 1.0f);
	}

	void onExitClicked() {
		gameService.EndGame(pool.CreateEntity());
	}

	void onLaserClicked() {
		eventService.Dispatch<GameActivateLaserEvent>(new GameActivateLaserEvent());
	}
}