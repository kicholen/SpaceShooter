using UnityEngine;

public class CollisionScript : MonoBehaviour {
	public int Damage;
	public int DamageTaken;

	void OnTriggerEnter2D(Component other) {
		DamageTaken += other.gameObject.GetComponent<CollisionScript>().Damage;
	}
}
