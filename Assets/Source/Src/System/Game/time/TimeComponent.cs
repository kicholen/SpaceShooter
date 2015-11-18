using Entitas;

public class TimeComponent : IComponent {
	public float deltaTime;
	public float gameDeltaTime;
	public float time;

	public float modificator;
	public bool isPaused;
}