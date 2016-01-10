using UnityEngine;

public interface IController {
	IServices Services { get; }
    GameObject GameObject { get; }
    MaterialStorage MaterialStorage { get;  }
}