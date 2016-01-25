using Entitas;

public class HomeMissileComponent : IComponent {
	public float delay;
	public float speed;

    /// <summary>
    /// Collision type of target. Value used when current FollowTarget dies.
    /// </summary>
    public int targetCollisionType = CollisionTypes.Unknown;
}