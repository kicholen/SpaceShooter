using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BonusesView : View, IView
{
    IBonusService bonusService;
    IViewService viewService;

    Transform content;

    public BonusesView(Pool pool, IViewService viewService, IBonusService bonusService) : base("EditorView/Bonus/BonusesView") {
        this.pool = pool;
        this.viewService = viewService;
        this.bonusService = bonusService;
    }

    public override void Init() {
        base.Init();
        content = getChild("Panel/Viewport/Content");
        bonusService.LoadBonusIds(onBonusesLoaded);
        addBackListener();
    }

    void onBonusesLoaded(Dictionary<long, string> difficultyIds) {
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
        bonusService.LoadBonusById(Convert.ToInt64(difficultyId), onBonusLoaded);
    }

    void addBackListener() {
        uiFactoryService.AddButton(getChild("BackButton").gameObject, () => goToLandingView());
    }

    void onBonusLoaded(BonusModelComponent component) {
        (viewService.SetView(ViewTypes.EDITOR_EDIT_BONUS) as EditBonusView).SetData(component);
    }

    void goToLandingView() {
        viewService.SetView(ViewTypes.EDITOR_LANDING);
    }

}
