using System.Collections.Generic;
using Entitas;

public class Services : IServices {
	IController controller;
	Pool pool;
	List<Updateable> updateables = new List<Updateable>();
	ILoadService loadService;
	EventService eventService;
	IGameService gameService;
	IViewService viewService;
	IUIFactoryService uiFactoryService;
    IWwwService wwwService;
    ISettingsService settingsService;
    ITranslationService translationService;
    IAnalyticsService analyticsService;
    IShipService shipService;
    ICurrencyService currencyService;
    IGamerService gamerService;
    IShopService shopService;
    IAPService iapService;
    IAdService adService;
    ITimeService timeService;

    public IController Controller { get { return controller; } }
	public Pool Pool { get { return pool; } }
	public List<Updateable> Updateables { get { return updateables; } }
	public ILoadService LoadService { get { return loadService;	} }
	public EventService EventService { get { return eventService; } }
	public IGameService GameService { get { return gameService; } }
	public IViewService ViewService { get { return viewService; } }
	public IUIFactoryService UIFactoryService { get { return uiFactoryService; } }
    public IWwwService WwwService { get { return wwwService; } }
    public ISettingsService SettingsService { get { return settingsService; } }
    public ITranslationService TranslationService { get { return translationService;  } }
    public IAnalyticsService AnalyticsService { get { return analyticsService;  } }
    public IShipService ShipService { get { return shipService;  } }
    public ICurrencyService CurrencyService { get { return currencyService; } }
    public IGamerService GamerService { get { return gamerService; } }
    public IShopService ShopService { get { return shopService; } }
    public IAPService IAPService { get { return iapService; } }
    public IAdService AdService { get { return adService; } }
    public ITimeService TimeService { get { return timeService; } }

    public Services(IController controller) {
        this.controller = controller;
        pool = Pools.pool;
        createServices();
        pool.CreateEntity()
            .AddMaterialReference(controller.MaterialStorage);
    }

    public void Init() {
        viewService.Init(this);
        currencyService.Init();
    }

    public void Update () {
		for (int i = 0; i < updateables.Count; i++) {
			updateables[i].Update();
		}
	}

    void createServices() {
        timeService = new TimeService();
        eventService = new EventService();
        uiFactoryService = new UIFactoryService();
        wwwService = controller.GameObject.AddComponent<WwwService>();
        viewService = new ViewService(eventService, uiFactoryService);
        loadService = new LoadService(eventService);
        gameService = new GameService(pool, viewService);
        settingsService = new SettingsService(pool);
        translationService = new TranslationService(settingsService);
        analyticsService = new AnalyticsService(settingsService);
        shipService = new ShipService(timeService, eventService);
        gamerService = new GamerService(eventService);
        currencyService = new CurrencyService(eventService, gamerService);
        iapService = new IAPService(eventService);
        adService = new AdService(currencyService);
        shopService = new ShopService(currencyService, eventService, iapService);
    }
}
