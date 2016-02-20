using Entitas;
using UnityEngine;

public class ActiveSystem : IExecuteSystem, ISetPool {
	Group camera;
	Group group;

    public void SetPool(Pool pool) {
		camera = pool.GetGroup(Matcher.Camera);
		group = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.NonRemovable, Matcher.Active));
	}
	
	public void Execute() {
		Rect rect = calculateCameraRect();
		foreach (Entity e in group.GetEntities()) {
			if (rect.Contains(e.position.pos)) {
				e.isNonRemovable = false;
				e.isActive = false;
			}
		}
	}

    Rect calculateCameraRect()
    {
        Entity cameraEntity = this.camera.GetSingleEntity();
        Camera camera = cameraEntity.camera.camera;
        float width = 0.0f;
        float height = 0.0f;
        Vector3 position = camera.transform.position;
        float size = camera.orthographicSize * 2.0f;
        float aspect = camera.aspect;
        if (camera.aspect < 1.0f)
        {
            height = size;
            width = size * aspect;
        }
        else
        {
            width = size;
            height = size * aspect;
        }

        return new Rect(position.x - width / 2.0f, position.y - height / 2.0f, width, height);
    }
}
