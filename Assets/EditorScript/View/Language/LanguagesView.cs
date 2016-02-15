using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class LanguagesView : View, IView
{
    ILanguageService languageService;
    IViewService viewService;

    Transform content;

    public LanguagesView(Pool pool, IViewService viewService, ILanguageService languageService) : base("EditorView/Language/LanguagesView")
    {
        this.pool = pool;
        this.viewService = viewService;
        this.languageService = languageService;
    }

    public override void Init()
    {
        base.Init();
        content = getChild("Panel/Viewport/Content");
        languageService.LoadLanguageIds(onLanguageIdsLoaded);
        addBackListener();
    }

    void onLanguageIdsLoaded(Dictionary<long, string> languageIds)
    {
        List<KeyValuePair<long, string>> list = languageIds.ToList();
        list.Sort((first, second) => first.Key.CompareTo(second.Key));
        list.RemoveAt(list.FindIndex(value => value.Value.Equals("pl")));
        foreach (KeyValuePair<long, string> pair in list)
            addButton(pair);
    }

    void addButton(KeyValuePair<long, string> pair)
    {
        GameObject go = uiFactoryService.CreatePrefab("EditorView/Button");
        uiFactoryService.AddText(go.transform, "Text", pair.Value);
        uiFactoryService.AddButton(go, onEnemyClicked);
        go.name = pair.Key.ToString();
        go.transform.SetParent(content.transform);
    }

    void onEnemyClicked()
    {
        string languageId = EventSystem.current.currentSelectedGameObject.name;
        languageService.LoadLanguageById(Convert.ToInt64(languageId), onLanguageLoaded);
    }

    void addBackListener()
    {
        uiFactoryService.AddButton(getChild("BackButton").gameObject, () => goToLandingView());
    }

    void onLanguageLoaded(LanguageModel model)
    {
        (viewService.SetView(ViewTypes.EDITOR_EDIT_LANGUAGE) as EditLanguageView).SetData(model, Utils.Deserialize<LanguageModel>("pl"));
    }

    void goToLandingView()
    {
        viewService.SetView(ViewTypes.EDITOR_LANDING);
    }
}
