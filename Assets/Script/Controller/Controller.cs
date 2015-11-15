using UnityEngine;

public class Controller : MonoBehaviour {

	IServices services;
	public IServices Services {
		get {
			return services;
		}
	}

	void Start () {
		services = new Services(this);
		(services as Services).TestInit();
	}

	void Update () {
		services.Update();
	}
}
