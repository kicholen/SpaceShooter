using UnityEngine;
using System.Collections.Generic;

public class CollisionScript : MonoBehaviour {
	public Queue<string> queue = new Queue<string>();

	void OnTriggerEnter2D(Collider2D other) {
		queue.Enqueue(other.name);
	}
}
