using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultiesView : View, IView
{
    IDifficultyService difficultyService;
    IViewService viewService;

    Transform content;

    public DifficultiesView(Pool pool, IViewService viewService, IDifficultyService difficultyService) : base("EditorView/Difficulty/DifficultiesView") {
        this.pool = pool;
        this.viewService = viewService;
        this.difficultyService = difficultyService;
    }

    public override void Init() {
        base.Init();
        content = getChild("Panel/Viewport/Content");
        difficultyService.LoadDifficultyIds(onDifficultiesLoaded);
        addBackListener();
    }

    void onDifficultiesLoaded(Dictionary<long, string> difficultyIds) {
        List<KeyValuePair<long, string>> list = difficultyIds.ToList();
        list.Sort((first, second) => first.Key.CompareTo(second.Key));
        foreach (KeyValuePair<long, string> pair in list)
            addButton(pair);
    }

    void addButton(KeyValuePair<long, string> pair) {
        GameObject go = uiFactoryService.CreatePrefab("EditorView/Button");
        uiFactoryService.AddText(go.transform, "Text", pair.Value);
        uiFactoryService.AddButton(go, onEnemyClicked);
        go.name = pair.Key.ToString();
        go.transform.SetParent(content.transform);
    }

    void onEnemyClicked() {
        string difficultyId = EventSystem.current.currentSelectedGameObject.name;
        difficultyService.LoadDifficultyById(Convert.ToInt64(difficultyId), onDifficultyLoaded);
    }

    void addBackListener() {
        uiFactoryService.AddButton(getChild("BackButton").gameObject, () => goToLandingView());
    }

    void onDifficultyLoaded(DifficultyModelComponent component) {
        (viewService.SetView(ViewTypes.EDITOR_EDIT_DIFFICULTY) as EditDifficultyView).SetData(component);
    }

    void goToLandingView() {
        viewService.SetView(ViewTypes.EDITOR_LANDING);
    }
}
