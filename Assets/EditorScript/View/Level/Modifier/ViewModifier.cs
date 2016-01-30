﻿using System.Collections.Generic;
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
        nullifyActiveGo();
        activeGo = hitGO;
        activeGo.GetComponent<SpriteRenderer>().color = Color.red;
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
        if (activeGo != null) {
            activeGo.GetComponent<SpriteRenderer>().color = Color.white;
            activeGo = null;
        }
        eventService.Dispatch<NoActiveModelEvent>(new NoActiveModelEvent());
    }

    void createNodeByTypeIfNotNone() {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        switch (type) {
            case SelectedType.Wave:
                AddWaveAction waveAction = new AddWaveAction(position.y);
                executor.Execute(waveAction);
                factory.CreateWaveElement(waveAction.getModel());
                break;
            case SelectedType.Enemy:
                AddEnemyAction enemyAction = new AddEnemyAction(position.y);
                executor.Execute(enemyAction);
                factory.CreateEnemyElement(enemyAction.getModel());
                break;
            case SelectedType.None:
            default:
                break;
        }
    }

    void removeActiveNodeIfShould() {
        if (isNodeRemovable()) {
            if (activeGo.GetComponent<EditableBehaviour>().waveModel != null) {
                executor.Execute(new RemoveWaveAction(activeGo.GetComponent<EditableBehaviour>().waveModel));
            }
            else {
                executor.Execute(new RemoveEnemyAction(activeGo.GetComponent<EditableBehaviour>().enemyModel));
            }
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