using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ViewModifier : MonoBehaviour {
    LevelActionExecutor executor;
    EditableElementsFactory factory;
    SelectedType type = SelectedType.None;
    GameObject dragging;

    public void SetExecutor(LevelActionExecutor executor) {
        this.executor = executor;
    }

    public void SetFactory(EditableElementsFactory factory) {
        this.factory = factory;
    }

    public void SetSelectedType(SelectedType type) {
        this.type = type;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && !isGuiHit()) {
            GameObject hitGO =getDraggingObjectIfHit();
            if (hitGO != null) {
                dragging = hitGO;
            }
            else if (dragging != null) {
                setDraggingToMouseY();
            }
            else {
                createNodeByTypeIfNotNone();
            }
        }
    }

    bool isGuiHit() {
        PointerEventData cursor = new PointerEventData(EventSystem.current);
        cursor.position = Input.mousePosition;
        List<RaycastResult> objectsHit = new List<RaycastResult>();
        EventSystem.current.RaycastAll(cursor, objectsHit);
        return objectsHit.Count > 0;
    }

    GameObject getDraggingObjectIfHit() {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (isHitColliderEditable(hit)) {
            return hit.collider.gameObject;
        }
        return null;
    }

    void setDraggingToMouseY() {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging.GetComponent<EditableBehaviour>().SetSpawnBarrier(position.y);
        dragging = null;
    }

    bool isHitColliderEditable(RaycastHit2D hit) {
        return hit.collider != null && hit.collider.GetComponent<EditableBehaviour>();
    }

    void createNodeByTypeIfNotNone() {
        if (type != SelectedType.None) {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AddWaveAction action = new AddWaveAction(position.y);
            executor.Execute(action);
            factory.CreateWaveElement(action.getModel());
        }
    }
}