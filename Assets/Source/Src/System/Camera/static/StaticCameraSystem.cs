using Entitas;
using System.Collections.Generic;

public class StaticCameraSystem : IReactiveSystem {
	public TriggerOnEvent trigger { get { return Matcher.StaticCamera.OnEntityAdded(); } }

	public void Execute(List<Entity> entities) {

	}
}