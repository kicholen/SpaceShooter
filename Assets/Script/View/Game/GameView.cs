using Entitas;

public class GameView : View, IView {
	
	IGameService gameService;
	
	public GameView(Pool pool, IUIFactoryService uiFactoryService, IGameService gameService, EventService eventService)
	: base(pool, uiFactoryService, eventService, "View/GameView") {
		this.gameService = gameService;
		uiFactoryService.AddButton(go.transform, "PauseButton", onPauseClicked);

		eventService.AddListener<GameSlowEvent>(onGameSlow);
		go.SetActive(false);
	}
	
	public override void Destroy() {
		base.Destroy();
		eventService.RemoveListener<GameSlowEvent>(onGameSlow);
	}

	public override void Hide() {
		OnHidden(null);
	}

	void onGameSlow(GameSlowEvent e) {
		go.SetActive(e.slow < 1.0f);
	}

	void onPauseClicked() {
		gameService.EndGame(null);
	}
}