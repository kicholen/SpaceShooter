using UnityEngine;

public class Controller : MonoBehaviour, IController {

	IServices services;
	public IServices Services {
		get {
			return services;
		}
	}

	void Start () {
		services = new Services(this);
		services.Init();
		services.LoadService.PrepareAndExecute(new InitPhase(services));
	}

	void Update () {
		services.Update();
	}
}
