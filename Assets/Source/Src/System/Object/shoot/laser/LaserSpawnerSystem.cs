using Entitas;
using UnityEngine;

public class LaserSpawnerSystem : IExecuteSystem, ISetPool {
	const float EPSILON = 0.005f;
    const float DEFAULT_LASER_HEIGHT = 10.0f;

    Pool pool;
	Group group;
	Group time;
	LayerMask enemyLayerMask;
	LayerMask playerLayerMask;

    public void SetPool(Pool pool) {
		this.pool = pool;
		group = pool.GetGroup(Matcher.LaserSpawner);
		time = pool.GetGroup(Matcher.Time);
		enemyLayerMask = (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Static"));
		playerLayerMask = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Static"));
	}

	public void Execute()
    {
        TimeComponent timeComponent = time.GetSingleEntity().time;
        if (isPaused(timeComponent))
           return;

        foreach (Entity e in group.GetEntities())
            update(e);
    }

    void update(Entity e)
    {
        LaserSpawnerComponent component = e.laserSpawner;
        float laserHeight = component.maxHeight;
        GameObject go = e.gameObject.gameObject;
        component.direction = Quaternion.Euler(0, 0, component.angle) * Vector2.up;
        Collider2D collider = findCollider(laserHeight, component, go);
        if (collider != null)
        {
            CollisionScript collision = collider.GetComponentInParent<CollisionScript>();
            Transform collidi = collider.transform;
            if (collision != null)
                collision.DamageTaken += component.damage;
            laserHeight = Vector2.Distance(collidi.position, go.transform.position);
        }
        createLaserIfNotExist(e, laserHeight, component);
        component.height = laserHeight;
    }

    void createLaserIfNotExist(Entity e, float laserHeight, LaserSpawnerComponent component)
    {
        if (component.laser == null)
        {
            Entity laser = pool.CreateEntity()
                .AddLaser(laserHeight, component.direction, e)
                .AddPosition(new Vector2().Set(e.position.pos))
                .AddResource(component.resource);
            laser.isNonRemovable = true;
            component.laser = laser;
        }
    }

    bool isPaused(TimeComponent timeComponent)
    {
        return timeComponent.isPaused;
    }

    Collider2D findCollider(float laserHeight, LaserSpawnerComponent component, GameObject go)
    {
        RaycastHit2D hit = Physics2D.Raycast(go.transform.position,
            component.direction,
            laserHeight,
            component.collisionType == CollisionTypes.Player ? enemyLayerMask : playerLayerMask);

        return hit.collider;
    }
}