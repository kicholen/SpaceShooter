using Entitas;
using System;
using System.Collections.Generic;

public class EditLanguageView : View, IView
{
    ILanguageService languageService;
    IViewService viewService;

    LanguageModel model;
    LanguageModel defaultModel;

    public EditLanguageView(Pool pool, IViewService viewService, ILanguageService languageService) : base("EditorView/Language/LanguageView")
    {
        this.pool = pool;
        this.viewService = viewService;
        this.languageService = languageService;
    }

    public override void Init()
    {
        base.Init();
        addSaveBackListeners();
    }

    public void SetData(LanguageModel model, LanguageModel defaultModel)
    {
        this.defaultModel = defaultModel;
        this.model = model;
        model.Init();
        defaultModel.Init();
        createHud();
    }

    void createHud()
    {
        new LanguagePanelHud(getChild("Panel/Viewport/Content"), model, defaultModel);
    }

    void addSaveBackListeners()
    {
        uiFactoryService.AddButton(getChild("SaveButton").gameObject, saveAndBack);
        uiFactoryService.AddButton(getChild("BackButton").gameObject, goToLandingView);
    }

    void saveAndBack()
    {
        model.translations.Clear();
        foreach (KeyValuePair<string, string> entry in model.data)
            model.translations.Add(new Translation(entry.Key, entry.Value));
        saveLanguage(goToLandingView);
    }

    void saveLanguage(Action onSaveSuccess)
    {
        languageService.UpdateLanguage(model, goToLandingView);
    }

    void goToLandingView()
    {
        viewService.SetView(ViewTypes.EDITOR_LANUGAGES);
    }
}
