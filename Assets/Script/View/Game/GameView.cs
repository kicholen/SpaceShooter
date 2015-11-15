using UnityEngine;

public class GameView : IView {
	
	EventService eventService;
	IGameService gameService;
	GameObject go;
	
	public GameObject Go { get { return go; } }
	
	public GameView(IUIFactoryService uiFactoryService, IGameService gameService, EventService eventService) {
		this.eventService = eventService;
		this.gameService = gameService;
		go = uiFactoryService.CreatePrefab("View/GameView");
		uiFactoryService.AddButton(go.transform, "PauseButton", onPauseClicked);

		eventService.AddListener<GameSlowEvent>(onGameSlow);
		go.SetActive(false);
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
		eventService.RemoveListener<GameSlowEvent>(onGameSlow);
		Object.Destroy(go);
	}

	void onGameSlow(GameSlowEvent e) {
		go.SetActive(e.slow < 1.0f);
	}

	void onPauseClicked() {
		gameService.EndGame();
	}
}