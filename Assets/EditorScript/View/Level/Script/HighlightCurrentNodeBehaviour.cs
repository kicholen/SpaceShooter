using UnityEngine;

public class HighlightCurrentNodeBehaviour : MonoBehaviour {
    Camera mainCamera;
    EditableBehaviour[] behaviours;

    void Start() {
        mainCamera = Camera.main;
        Debug.Log("start");
        behaviours = FindObjectsOfType<EditableBehaviour>();
    }

    void Update() {
        float y = mainCamera.transform.position.y;
        foreach (EditableBehaviour behaviour in behaviours)
            behaviour.GetComponent<SpriteRenderer>().color = behaviour.SpawnBarrier < y ? Color.green : Color.white;
    }
}