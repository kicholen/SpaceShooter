public class EditorViewFactory : ViewFactory {

    public EditorViewFactory(IServices services) {
        Init(services);
    }

    public override IView Create(ViewTypes type) {
        IView view = null;
        switch (type) {
            case ViewTypes.EDITOR_PATH:
                view = new PathView((services as EditorServices).PathService, services.ViewService);
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
