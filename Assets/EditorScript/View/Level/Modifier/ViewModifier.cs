using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ViewModifier : MonoBehaviour {
    LevelActionExecutor executor;
    EditableElementsFactory factory;
    SelectedType type = SelectedType.None;
    EventService eventService;

    GameObject activeGo;

    public void SetExecutor(LevelActionExecutor executor) {
        this.executor = executor;
    }

    public void SetFactory(EditableElementsFactory factory) {
        this.factory = factory;
    }

    public void SetSelectedType(SelectedType type) {
        this.type = type;
    }

    public void SetEventService(EventService eventService) {
        this.eventService = eventService;
    }

    void Update() {
        createNewNodeIfCan();
        removeActiveNodeIfShould();
    }

    void createNewNodeIfCan() {
        if (Input.GetMouseButtonDown(0) && !isGuiHit()) {
            GameObject hitGO = getDraggingObjectIfHit();
            if (hitGO != null) {
                setActiveGo(hitGO);
            }
            else if (activeGo != null) {
                setDraggingToMouseY();
            }
            else {
                createNodeByTypeIfNotNone();
            }
        }
    }

    GameObject getDraggingObjectIfHit() {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (isHitColliderEditable(hit)) {
            return hit.collider.gameObject;
        }
        return null;
    }

    bool isGuiHit() {
        PointerEventData cursor = new PointerEventData(EventSystem.current);
        cursor.position = Input.mousePosition;
        List<RaycastResult> objectsHit = new List<RaycastResult>();
        EventSystem.current.RaycastAll(cursor, objectsHit);
        return objectsHit.Count > 0;
    }

    void setActiveGo(GameObject hitGO) {
        activeGo = hitGO;
        if (activeGo.GetComponent<EditableBehaviour>().waveModel != null)
            eventService.Dispatch<ActiveWaveModelChangeEvent>(new ActiveWaveModelChangeEvent(activeGo.GetComponent<EditableBehaviour>().waveModel));
        else
            eventService.Dispatch<ActiveEnemyModelChangeEvent>(new ActiveEnemyModelChangeEvent(activeGo.GetComponent<EditableBehaviour>().enemyModel));
    }

    void setDraggingToMouseY() {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        activeGo.GetComponent<EditableBehaviour>().SetSpawnBarrier(position.y);
        nullifyActiveGo();
    }

    void nullifyActiveGo() {
        activeGo = null;
        eventService.Dispatch<NoActiveModelEvent>(new NoActiveModelEvent());
    }

    void createNodeByTypeIfNotNone() {
        if (type != SelectedType.None) {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AddWaveAction action = new AddWaveAction(position.y);
            executor.Execute(action);
            factory.CreateWaveElement(action.getModel());
        }
    }

    void removeActiveNodeIfShould() {
        if (isNodeRemovable()) {
            executor.Execute(activeGo.GetComponent<EditableBehaviour>().waveModel != null
                ? new RemoveWaveAction(activeGo.GetComponent<EditableBehaviour>().waveModel)
                : new RemoveWaveAction(activeGo.GetComponent<EditableBehaviour>().waveModel));
            Destroy(activeGo);
            nullifyActiveGo();
        }
    }

    bool isNodeRemovable() {
        return Input.GetKey(KeyCode.Delete) && activeGo != null;
    }

    bool isHitColliderEditable(RaycastHit2D hit) {
        return hit.collider != null && hit.collider.GetComponent<EditableBehaviour>();
    }
}