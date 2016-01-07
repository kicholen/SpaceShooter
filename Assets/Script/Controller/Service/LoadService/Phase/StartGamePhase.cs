using System.Collections.Generic;
using System;

public class StartGamePhase : Phase, IPhase
{
	static bool isFirstGame = true;

	IViewService viewService;
	IGameService gameService;
	int level;

	public StartGamePhase(IViewService viewService, IGameService gameService, int level) {
		this.viewService = viewService;
		this.gameService = gameService;
		this.level = level;
	}

	public override void CreateActions() {
		actions = new Queue<Action>();
		actions.Enqueue(() => addViewListener());
		actions.Enqueue(() => viewService.SetView(ViewTypes.LOAD));
		actions.Enqueue(() => { gameService.InitGame(level); nextAction(); });
		attachFirstGameActionsIfShould();
		actions.Enqueue(() => addViewListener());
		actions.Enqueue(() => viewService.SetView(ViewTypes.GAME));
		actions.Enqueue(() => { gameService.PlayGame(); nextAction(); });
	}

	void attachFirstGameActionsIfShould() {
		if (isFirstGame) {
			actions.Enqueue(() => { gameService.InitPool(Resource.Explosion, 4); nextAction(); });
			actions.Enqueue(() => { gameService.InitPool(Resource.ExplosionMissile, 10); nextAction(); });
			actions.Enqueue(() => { gameService.InitPool(ResourceWithColliders.MissileEnemy, 20); nextAction(); });
			actions.Enqueue(() => { gameService.InitPool(ResourceWithColliders.MissilePrimary, 20); nextAction(); });
			actions.Enqueue(() => { gameService.InitPool(ResourceWithColliders.MissileSecondary, 40); nextAction(); });
			actions.Enqueue(() => { gameService.InitPool(ResourceWithColliders.Enemy, 10); nextAction(); });
			actions.Enqueue(() => { gameService.InitPool(ResourceWithColliders.Star, 10); nextAction(); });
			actions.Enqueue(() => { gameService.InitPool(ResourceWithColliders.Bonus, 3); nextAction(); });
			isFirstGame = false;
		}
	}
}

