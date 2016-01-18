using UnityEngine;

public class RightBottomPanelHud : BaseGui {
    Transform content;
    EventService eventService;

    RightBottomPanelWaveViewUpdater waveUpdater;
    RightBottomPanelEnemyViewUpdater enemyUpdater;

    public RightBottomPanelHud(Transform content, EventService eventService) {
        go = content.gameObject;
        this.eventService = eventService;
        prepareHud();
        createUpdaters();
        addListeners();
    }

    private void createUpdaters() {
        waveUpdater = new RightBottomPanelWaveViewUpdater();
        enemyUpdater = new RightBottomPanelEnemyViewUpdater();
    }

    public override void Destroy() {
        base.Destroy();
        removeListeners();
    }

    void prepareHud() {
        content = getChild("Viewport/Content");
        hideView();
    }

    void hideView() {
        go.SetActive(false);
    }

    void showView() {
        go.SetActive(true);
    }

    void addListeners() {
        eventService.AddListener<NoActiveModelEvent>(onNoActiveModel);
        eventService.AddListener<ActiveWaveModelChangeEvent>(onWaveModelChange);
        eventService.AddListener<ActiveEnemyModelChangeEvent>(onEnemyModelChange);
    }

    void onNoActiveModel(NoActiveModelEvent gameEvent) {
        hideView();
    }

    void onWaveModelChange(ActiveWaveModelChangeEvent gameEvent) {
        showAndClearView();
        waveUpdater.Update(content, gameEvent.model);
    }

    void onEnemyModelChange(ActiveEnemyModelChangeEvent gameEvent) {
        showAndClearView();
        enemyUpdater.Update(content, gameEvent.model);
    }

    void showAndClearView() {
        showView();
        removeChildren(content.gameObject);
    }

    void removeListeners() {
        eventService.RemoveListener<NoActiveModelEvent>(onNoActiveModel);
        eventService.RemoveListener<ActiveWaveModelChangeEvent>(onWaveModelChange);
        eventService.RemoveListener<ActiveEnemyModelChangeEvent>(onEnemyModelChange);
    }
}