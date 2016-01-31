using System.Collections.Generic;
using UnityEngine;

public class CollisionScriptExtended : MonoBehaviour {
    public List<Vector2> Position = new List<Vector2>();

    void OnTriggerEnter2D(Component other) {
        Position.Add(other.gameObject.transform.position);
    }
}
