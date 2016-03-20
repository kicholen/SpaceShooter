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
			case ViewTypes.LOAD:
				view = new LoadView();
			break;
            case ViewTypes.MAIN:
                view = new MainView(services);
                break;
            case ViewTypes.RESULTS:
                view = new ResultsView(services);
                break;
            case ViewTypes.LEVEL_UP:
                view = new LevelUpView(services);
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