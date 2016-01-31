using System.Collections.Generic;
using UnityEngine;

public class CollisionScriptExtended : MonoBehaviour {
    public Queue<Vector2> Position = new Queue<Vector2>();

    void OnCollisionEnter2D(Collision2D collision) {
        Position.Enqueue(collision.contacts[0].point);
    }
}
