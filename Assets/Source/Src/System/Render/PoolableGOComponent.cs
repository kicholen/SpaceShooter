using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PoolableGOComponent : IComponent {
	public string name;
	public Queue<GameObject> queue;
}