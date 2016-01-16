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
                view = new LevelsView((services as EditorServices).LevelService, services.ViewService, (services as EditorServices).PathService);
                break;
            case ViewTypes.EDITOR_EDIT_LEVEL:
                view = new EditLevelView((services as EditorServices).LevelService, services.ViewService, (services as EditorServices).PathService);
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
