using Entitas;
using Entitas.Unity.VisualDebugging;

public class GameService : IGameService {
	IViewService viewService;
    Systems systems;
	Pool pool;

	public GameService(Pool pool, IViewService viewService) {
		this.viewService = viewService;
		this.pool = pool;
		systems = CreateSystems();
	}

	public void Init(IServices services) {
		services.EventService.AddListener<GameEndedEvent>(onGameEnded);
		pool.CreateEntity()
			.AddEventService(services.EventService)
			.AddUIFactoryService(services.UIFactoryService)
			.AddCanvas(services.ViewService.Canvas);
			
		systems.Initialize();
		services.Updateables.Add(this);
	}

	public void Update() {
		systems.Execute();
	}

	public void InitGame(int level) {
		pool.CreateEntity()
			.AddStartGame(level);
	}

	public void PlayGame() {
		pool.CreateEntity()
			.IsPauseGame(true);
	}

	public void InitPool(string resource, int count) {
		for (int i = 0; i < count; i++) {
			pool.CreateEntity()
				.AddResource(resource)
				.IsDestroyEntity(true);
		}
	}

	public void EndGame(Entity e) {
		e.isDestroyEntity = true;
		viewService.SetView(ViewTypes.LANDING);
		pool.CreateEntity()
			.IsEndGame(true);
	}

	void onGameEnded(GameEndedEvent e) {
		pool.CreateEntity()
			.AddDelayedCall(2.0f, EndGame);
	}

    Systems CreateSystems() {
#if (UNITY_EDITOR)
        return new DebugSystems()
        .Add(pool.CreateTestSystem())
#else
        return new Systems()
#endif

            .Add(pool.CreateTimeSystem())

                // Initialize
                .Add(pool.CreateCreateBonusSystem())
                .Add(pool.CreateCreatePathSystem())
                .Add(pool.CreateCreateEnemySystem())
                .Add(pool.CreateCreateDifficultySystem())
                .Add(pool.CreateCreateSettingsSystem())
                .Add(pool.CreateCreateShipSystem())
                .Add(pool.CreateWeaponSystem())
                .Add(pool.CreateCreateCameraSystem())
                .Add(pool.CreateCreateLevelSystem())
                .Add(pool.CreateCreateGridSystem())

                // Settings stuff
                .Add(pool.CreateDifficultyControllerSystem())

                // Spawners
                .Add(pool.CreateSpawnMissileSystem())
                .Add(pool.CreateMultipleMissileSpawnerSystem())
                .Add(pool.CreateCircleMissileSpawnerSystem())
                .Add(pool.CreateCircleMissileRotatedSpawnerSystem())
                .Add(pool.CreateHomeMissileSpawnerSystem())
                .Add(pool.CreateFindTargetSystem())
                .Add(pool.CreateHomeMissileSystem())
                .Add(pool.CreateEnemySpawnerSystem())
                .Add(pool.CreateWaveSpawnerSystem())
                .Add(pool.CreateDispersionMissileSpawnerSystem())
                .Add(pool.CreateTargetMissileSpawnerSystem())
                .Add(pool.CreateTargetMissileSystem())
                .Add(pool.CreateAtomBombSystem())

            // AddGO
            .Add(pool.CreateAddGameObjectSystem())
            .Add(pool.CreateAddUIGameObjectSystem())
            .Add(pool.CreateAddCollisionToGameObjectSystem())
            .Add(pool.CreateAddCollisionPositionToGOSystem())

            // Sort order
            .Add(pool.CreateSortOrderSystem())

            // Input
            .Add(pool.CreateCreateMouseInputSystem())
            .Add(pool.CreateProcessInputSystem())

                // Input player
                .Add(pool.CreatePlayerInputSystem())

                // magnet
                .Add(pool.CreateMagnetSystem())

                // AI
                .Add(pool.CreateFirstBossSystem())

                // special enemies
                .Add(pool.CreateMotherShipSystem())
                .Add(pool.CreateMovingBlockadeSystem())

            // Tween
            .Add(pool.CreateTweenSystem())
            .Add(pool.CreateDelayedCallSystem())

            // Physics
            .Add(pool.CreateAccelerationSystem())
            .Add(pool.CreateVelocitySystem())
            .Add(pool.CreateCollisionSystem())
            .Add(pool.CreateMoveWithCameraSystem())
            .Add(pool.CreatePathSystem())
            .Add(pool.CreateHelperShipSystem())
            .Add(pool.CreatePositionSystem())
            .Add(pool.CreateRotateSystem())

                // laser
                .Add(pool.CreateLaserSpawnerSystem())
                .Add(pool.CreateLaserSystem())

                // Physics not so much
                .Add(pool.CreateSnapPositionSystem())
                .Add(pool.CreateRemoveOutOfViewGOSystem())
                .Add(pool.CreateFaceDirectionSystem())
                .Add(pool.CreateActiveSystem())
                .Add(pool.CreateGridSystem())
                .Add(pool.CreatePlayerHealthBarSystem())
                .Add(pool.CreateIndicatorSystem())

                // Object
                .Add(pool.CreateHealthSystem())
                .Add(pool.CreateDeadPlayerSystem())
                .Add(pool.CreateBackgroundSystem())

                // OnDeath Actions
                .Add(pool.CreateBonusOnDeathSystem())
                .Add(pool.CreateCameraShakeOnDeathSystem())
                .Add(pool.CreateParticlesOnDeathSystem())
                .Add(pool.CreateSoundOnDeathSystem())
                .Add(pool.CreateExplosionOnDeathSystem())
                .Add(pool.CreateTweenOnDeathSystem())

                // Effects
                .Add(pool.CreateAlphaSystem())
                .Add(pool.CreateGhostSystem())
                .Add(pool.CreateShakeSystem())
                .Add(pool.CreateParticleSpawnSystem())
                .Add(pool.CreateShieldCollisionSystem())
                .Add(pool.CreateShieldCollisionEffectSystem())
                .Add(pool.CreateTrailSystem())

                // bonus
                .Add(pool.CreateActivateBonusSystem())
                .Add(pool.CreateSpeedBonusSystem())

            // RelativePositionGO
            .Add(pool.CreateRelativePositionSystem())

            // PositionGO
            .Add(pool.CreatePositionGameObjectSystem())

            .Add(pool.CreateStartGameSystem())

            // RemoveGO
            .Add(pool.CreateRemoveGameObjectSystem())

            // Camera
            .Add(pool.CreateDefaultCameraSystem())
            .Add(pool.CreateSmoothCameraSystem())

            // Stats
            .Add(pool.CreateGameStatsSystem())

            // Sound
            .Add(pool.CreateSoundSystem())

            // Destroy
            .Add(pool.CreateDestroyEntitySystem())

            .Add(pool.CreatePauseGameSystem())
            .Add(pool.CreateEndGameSystem())
            .Add(pool.CreateSlowGameSystem())
            .Add(pool.CreateCallOnFrameEndSystem())
            .Add(pool.CreateDestroyEntityDelayedSystem());
    }
}
