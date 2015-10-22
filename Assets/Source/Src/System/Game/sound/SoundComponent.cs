using Entitas;
using UnityEngine;

public class SoundComponent : IComponent {
	public string path;
	public float volume; // [0.0f, 1.0f]
	public GameObject go;
}