using UnityEngine;

public class CollisionScript : MonoBehaviour {
	public int Damage;
	public int DamageTaken;

	void OnCollisionEnter2D(Collision2D collision) {
		DamageTaken += collision.collider.GetComponent<CollisionScript>().Damage;
	}
}
