using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class CreateLevelSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.CreateLevel.OnEntityAdded(); } }

	Pool pool;
	Group group;
	Group players;
	Group cameras;

	public void SetPool(Pool pool) {
		this.pool = pool;
        group = this.pool.GetGroup(Matcher.LevelModel);
        players = this.pool.GetGroup(Matcher.Player);
        cameras = this.pool.GetGroup(Matcher.Camera);
	}
	
	public void Execute(List<Entity> entities)
    {
        Entity e = entities.SingleEntity();
        Entity cameraEntity = cameras.GetSingleEntity();
        Entity player = players.GetSingleEntity();
        CreateLevelComponent createLevel = e.createLevel;
        LevelModelComponent component = createLevelComponent(createLevel);
        Vector2 screenSize = getScreenSize(cameraEntity);

        addCameraSnapPositionIfNotExist(component, cameraEntity);
        player.AddSnapPosition(component.position.x, component.position.y, screenSize.x, screenSize.y, true);
        createBackground(screenSize.x, screenSize.y);
        createShipBonus();

        e.isDestroyEntity = true;
    }

    void createShipBonus()
    {
        pool.CreateEntity()
            .AddShipBonus(0.0f, 0.0f);
    }

    Vector2 getScreenSize(Entity cameraEntity)
    {
        Camera camera = cameraEntity.camera.camera;
        Vector2 screenSize = new Vector2(camera.orthographicSize * camera.aspect * 2.0f, camera.orthographicSize * 2.0f);
        return screenSize;
    }

    void addCameraSnapPositionIfNotExist(LevelModelComponent component, Entity cameraEntity)
    {
        if (!cameraEntity.hasSnapPosition)
        {
            cameraEntity.AddSnapPosition(component.position.x, component.position.y, component.size.x, component.size.y, false);
        }
    }

    LevelModelComponent createLevelComponent(CreateLevelComponent createLevel)
    {
        LevelModelComponent component = getLevelModelIfExists(createLevel.level);
        if (component == null)
        {
            component = Utils.Deserialize<LevelModelComponent>(createLevel.level.ToString());
            pool.CreateEntity()
                .AddComponent(ComponentIds.LevelModel, component);
        }
        component.enemyIndex = 0;
        component.waveIndex = 0;

        pool.CreateEntity()
            .AddEnemySpawner(component);
        return component;
    }

    LevelModelComponent getLevelModelIfExists(int id) {
		foreach (Entity e in group.GetEntities()) {
			if (id == e.levelModel.id) {
				return e.levelModel;
			}
		}
		return null;
	}

	void createBackground(float width, float height) {
		pool.CreateEntity()
			.IsMoveWithCamera(true)
			.AddVelocity(new Vector2())
			.AddVelocityLimit(0.0f)
			.AddSortOrder(SortTypes.Background)
			.AddPosition(new Vector2())
			.AddResource(Resource.Background)
			.AddBackground(Color.black, 20, new Vector2(width, height));
	}
}