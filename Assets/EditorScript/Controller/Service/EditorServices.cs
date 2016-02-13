using System.Collections.Generic;
using Entitas;

public class EditorServices : IServices {
	IController controller;
	Pool pool;
	List<Updateable> updateables = new List<Updateable>();
	ILoadService loadService;
	EventService eventService;
	IGameService gameService;
	IViewService viewService;
	IUIFactoryService uiFactoryService;
    IPathService pathService;
    ILevelService levelService;
    IEnemyService enemyService;
    IBonusService bonusService;
    IDifficultyService difficultyService;
    IWwwService wwwService;
    IInfoService infoService;

    public IController Controller { get { return controller; } }
	public Pool Pool { get { return pool; } }
	public List<Updateable> Updateables { get { return updateables; } }
	public ILoadService LoadService { get { return loadService;	} }
	public EventService EventService { get { return eventService; } }
	public IGameService GameService { get { return gameService; } }
	public IViewService ViewService { get { return viewService; } }
	public IUIFactoryService UIFactoryService { get { return uiFactoryService; } }
    public IPathService PathService { get { return pathService; } }
    public ILevelService LevelService { get { return levelService; } }
    public IEnemyService EnemyService { get { return enemyService; } }
    public IBonusService BonusService { get { return bonusService; } }
    public IDifficultyService DifficultyService { get { return difficultyService; } }
    public IWwwService WwwService { get { return wwwService; } }
    public IInfoService InfoService { get { return infoService; } }

    public EditorServices(IController controller) {
        this.controller = controller;
        pool = Pools.pool;
        createServices();
        pool.CreateEntity()
            .AddMaterialReference(controller.MaterialStorage);
    }

    private void createServices() {
        eventService = new EventService();
        uiFactoryService = new UIFactoryService();
        wwwService = controller.GameObject.AddComponent<WwwService>();
        viewService = new ViewService(eventService, uiFactoryService);
        loadService = new LoadService(eventService);
        gameService = new GameService(pool, viewService);
        pathService = new PathService(pool, wwwService, eventService);
        levelService = new LevelService(wwwService, eventService);
        enemyService = new EnemyService(pool, wwwService, eventService);
        bonusService = new BonusService(pool, wwwService, eventService);
        difficultyService = new DifficultyService(pool, wwwService, eventService);
        infoService = new InfoService(viewService, uiFactoryService, eventService);
        updateables.Add(infoService);
    }

    public void Init() {
        gameService.Init(this);
        viewService.Init(this);
        ViewService.SetView(ViewTypes.EDITOR_LANDING);
    }
	
	public void Update () {
		for (int i = 0; i < updateables.Count; i++) {
			updateables[i].Update();
		}
	}
}
