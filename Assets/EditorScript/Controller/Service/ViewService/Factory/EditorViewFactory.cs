public class EditorViewFactory : ViewFactory {

    public EditorViewFactory(IServices services) {
        Init(services);
    }

    public override IView Create(ViewTypes type) {
        IView view = null;
        switch (type) {
            case ViewTypes.EDITOR_LANDING:
                view = new EditorView(services.ViewService);
                break;
            case ViewTypes.EDITOR_PATH:
                view = new PathView((services as EditorServices).PathService, services.ViewService);
                break;
            case ViewTypes.EDITOR_EDIT_PATH:
                view = new EditPathView((services as EditorServices).PathService, services.ViewService);
                break;
            case ViewTypes.EDITOR_LEVELS:
                view = new LevelsView((services as EditorServices).LevelService, services.ViewService,
                    (services as EditorServices).PathService, (services as EditorServices).EnemyService,
                    (services as EditorServices).BonusService, (services as EditorServices).DifficultyService);
                break;
            case ViewTypes.EDITOR_EDIT_LEVEL:
                view = new EditLevelView(services.Pool, (services as EditorServices).LevelService, services.ViewService, (services as EditorServices).PathService);
                break;
            case ViewTypes.EDITOR_EDIT_ENEMY:
                view = new EditEnemyView(services.Pool, services.ViewService, (services as EditorServices).EnemyService);
                break;
            case ViewTypes.EDITOR_ENEMIES:
                view = new EnemiesView(services.Pool, services.ViewService, (services as EditorServices).EnemyService);
                break;
            case ViewTypes.EDITOR_EDIT_DIFFICULTY:
                view = new EditDifficultyView(services.Pool, services.ViewService, (services as EditorServices).DifficultyService);
                break;
            case ViewTypes.EDITOR_DIFFICULTIES:
                view = new DifficultiesView(services.Pool, services.ViewService, (services as EditorServices).DifficultyService);
                break;
            case ViewTypes.EDITOR_EDIT_BONUS:
                view = new EditBonusView(services.Pool, services.ViewService, (services as EditorServices).BonusService);
                break;
            case ViewTypes.EDITOR_BONUSES:
                view = new BonusesView(services.Pool, services.ViewService, (services as EditorServices).BonusService);
                break;
            case ViewTypes.EDITOR_EDIT_LANGUAGE:
                view = new EditLanguageView(services.Pool, services.ViewService, (services as EditorServices).LanguageService);
                break;
            case ViewTypes.EDITOR_LANUGAGES:
                view = new LanguagesView(services.Pool, services.ViewService, (services as EditorServices).LanguageService);
                break;
        }
        if (view == null) {
            view = base.Create(type);
        }
        else {
            initView(view);
        }

        return view;
    }
}
