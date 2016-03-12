using Entitas;
using UnityEngine;

public class GameObjectComponent : IComponent {
    public GameObject gameObject;
	public string path;
	public bool isPoolable;
}