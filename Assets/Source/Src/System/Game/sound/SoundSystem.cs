using Entitas;
using UnityEngine;

public class SoundSystem : IExecuteSystem, ISetPool {
	Group _group;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.Sound);
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			SoundComponent sound = e.sound;
			GameObject go = sound.go;
			if (go == null) {
				sound.go = new GameObject();
				AudioSource source = sound.go.AddComponent<AudioSource>();
				source.volume = sound.volume;
				source.clip = Resources.Load<AudioClip>(sound.path);
				source.Play();
			}
			else {
				AudioSource source = go.GetComponent<AudioSource>();
				if (!source.isPlaying) {
					UnityEngine.Object.Destroy(go);
					e.isDestroyEntity = true;
				}
			}
		}
	}
}