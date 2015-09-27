using UnityEngine;
using System.Collections.Generic;

public class CollisionScript : MonoBehaviour {
	public Queue<string> queue = new Queue<string>();

	void OnTiggerEnter2D(Collider2D other) {
		queue.Enqueue(other.name);
	}

	void OnCollisionEnter2D(Collision2D other) {
		queue.Enqueue(other.gameObject.name);
	}
}
