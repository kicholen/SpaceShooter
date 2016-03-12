using Entitas;

public class TimeComponent : IComponent {
	public float deltaTime;
	public float gameDeltaTime;
	public float gameTime;

	public float modificator;
	public bool isPaused;
}