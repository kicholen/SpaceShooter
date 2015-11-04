using Entitas;

public class HomeMissileComponent : IComponent {
	public float random;

	/// <summary>
	/// Collision type of target. Value used when current FollowTarget dies.
	/// </summary>
	public int targetCollisionType = CollisionTypes.Unknown;
}