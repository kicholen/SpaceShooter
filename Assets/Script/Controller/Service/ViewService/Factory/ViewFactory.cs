public class ViewFactory : IViewFactory {

	protected IServices services;

	public void Init(IServices services) {
		this.services = services;
	}

	public virtual IView Create(ViewTypes type) {
		IView view = null;
		switch (type) {
			case ViewTypes.GAME:
				view = new GameView(services.Updateables, services.GameService);
			break;
			case ViewTypes.INIT:
				view = new InitView();
			break;
			case ViewTypes.LANDING:
				view = new LandingView(services.ViewService);
			break;
			case ViewTypes.LEVEL:
				view = new LevelView(services.LoadService, services.ViewService, services.GameService);
			break;
			case ViewTypes.LOAD:
				view = new LoadView();
			break;
			case ViewTypes.SHIP:
				view = new ShipView(services.ViewService);
			break;
            case ViewTypes.SETTINGS:
                view = new SettingsView(services.SettingsService, services.ViewService);
                break;
        }
        initView(view);

        return view;
	}

    protected void initView(IView view) {
        view.SetBaseServices(services.UIFactoryService, services.EventService, services.TranslationService);
        view.SetPool(services.Pool);
        view.Init();
    }
}