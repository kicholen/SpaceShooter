using UnityEngine;
using UnityEngine.UI;

public class InfoService : IInfoService {
    const float textVisibleDuration = 5.0f;
    GameObject infoGo;
    float time = 0.0f;

    public InfoService(IViewService viewService, IUIFactoryService uiFactoryService, EventService eventService) {
        createInfoGameObject(viewService, uiFactoryService);
        addListener(eventService);
    }

    public void ShowInfo(string text) {
        infoGo.SetActive(true);
        infoGo.GetComponentInChildren<Text>().text = text;
        time = textVisibleDuration;
    }

    public void Update() {
        if (infoGo.activeSelf) {
            time -= Time.deltaTime;
            if (time < 0.0f) {
                infoGo.SetActive(false);
            }
        }
    }

    void createInfoGameObject(IViewService viewService, IUIFactoryService uiFactoryService) {
        infoGo = uiFactoryService.CreatePrefab("EditorView/InfoText");
        infoGo.transform.SetParent(viewService.Canvas.transform, false);
        infoGo.SetActive(false);
    }

    void addListener(EventService eventService) {
        eventService.AddListener<InfoBoxShowEvent>(onInfoBoxShowEvent);
    }

    void onInfoBoxShowEvent(InfoBoxShowEvent e) {
        ShowInfo(e.text);
    }
}
