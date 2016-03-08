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
       // .Add(pool.CreateTestSystem())
#else
        return new Systems()
#endif

            .Add(pool.CreateSystem<TimeSystem>())

                // Initialize
                .Add(pool.CreateSystem<CreateBonusSystem>())
                .Add(pool.CreateSystem<CreatePathSystem>())
                .Add(pool.CreateSystem<CreateEnemySystem>())
                .Add(pool.CreateSystem<CreateDifficultySystem>())
                .Add(pool.CreateSystem<CreateSettingsSystem>())
                .Add(pool.CreateSystem<CreateShipSystem>())
                .Add(pool.CreateSystem<WeaponSystem>())
                .Add(pool.CreateSystem<CreateCameraSystem>())
                .Add(pool.CreateSystem<CreateLevelSystem>())
                .Add(pool.CreateSystem<CreateGridSystem>())

                // Settings stuff
                .Add(pool.CreateSystem<DifficultyControllerSystem>())

                // Spawners
                .Add(pool.CreateSystem<SpawnMissileSystem>())
                .Add(pool.CreateSystem<MultipleMissileSpawnerSystem>())
                .Add(pool.CreateSystem<CircleMissileSpawnerSystem>())
                .Add(pool.CreateSystem<CircleMissileRotatedSpawnerSystem>())
                .Add(pool.CreateSystem<HomeMissileSpawnerSystem>())
                .Add(pool.CreateSystem<FindTargetSystem>())
                .Add(pool.CreateSystem<HomeMissileSystem>())
                .Add(pool.CreateSystem<EnemySpawnerSystem>())
                .Add(pool.CreateSystem<WaveSpawnerSystem>())
                .Add(pool.CreateSystem<DispersionMissileSpawnerSystem>())
                .Add(pool.CreateSystem<TargetMissileSpawnerSystem>())
                .Add(pool.CreateSystem<TargetMissileSystem>())
                .Add(pool.CreateSystem<AtomBombSystem>())

            // AddGO
            .Add(pool.CreateSystem<AddGameObjectSystem>())
            .Add(pool.CreateSystem<AddUIGameObjectSystem>())
            .Add(pool.CreateSystem<AddCollisionToGameObjectSystem>())
            .Add(pool.CreateSystem<AddCollisionPositionToGOSystem>())

            // Sort order
            .Add(pool.CreateSystem<SortOrderSystem>())

            // Input
            .Add(pool.CreateSystem<CreateMouseInputSystem>())
            .Add(pool.CreateSystem<ProcessInputSystem>())

                // Input player
                .Add(pool.CreateSystem<PlayerInputSystem>())

                // magnet
                .Add(pool.CreateSystem<MagnetSystem>())

                // AI
                .Add(pool.CreateSystem<FirstBossSystem>())

                // special enemies
                .Add(pool.CreateSystem<MotherShipSystem>())
                .Add(pool.CreateSystem<MovingBlockadeSystem>())

            // Tween
            .Add(pool.CreateSystem<TweenSystem>())
            .Add(pool.CreateSystem<DelayedCallSystem>())

            // Physics
            .Add(pool.CreateSystem<AccelerationSystem>())
            .Add(pool.CreateSystem<VelocitySystem>())
            .Add(pool.CreateSystem<CollisionSystem>())
            .Add(pool.CreateSystem<MoveWithCameraSystem>())
            .Add(pool.CreateSystem<PathSystem>())
            .Add(pool.CreateSystem<HelperShipSystem>())
            .Add(pool.CreateSystem<PositionSystem>())
            .Add(pool.CreateSystem<RotateSystem>())

                // laser
                .Add(pool.CreateSystem<LaserSpawnerSystem>())
                .Add(pool.CreateSystem<LaserSystem>())

                // Physics not so much
                .Add(pool.CreateSystem<SnapPositionSystem>())
                .Add(pool.CreateSystem<RemoveOutOfViewGOSystem>())
                .Add(pool.CreateSystem<FaceDirectionSystem>())
                .Add(pool.CreateSystem<ActiveSystem>())
                .Add(pool.CreateSystem<GridSystem>())
                .Add(pool.CreateSystem<PlayerHealthBarSystem>())
                .Add(pool.CreateSystem<IndicatorSystem>())

                // Object
                .Add(pool.CreateSystem<HealthSystem>())
                .Add(pool.CreateSystem<DeadPlayerSystem>())
                .Add(pool.CreateSystem<BackgroundSystem>())

                // OnDeath Actions
                .Add(pool.CreateSystem<BonusOnDeathSystem>())
                .Add(pool.CreateSystem<CameraShakeOnDeathSystem>())
                .Add(pool.CreateSystem<ParticlesOnDeathSystem>())
                .Add(pool.CreateSystem<SoundOnDeathSystem>())
                .Add(pool.CreateSystem<ExplosionOnDeathSystem>())
                .Add(pool.CreateSystem<TweenOnDeathSystem>())

                // Effects
                .Add(pool.CreateSystem<AlphaSystem>())
                .Add(pool.CreateSystem<GhostSystem>())
                .Add(pool.CreateSystem<ShakeSystem>())
                .Add(pool.CreateSystem<ParticleSpawnSystem>())
                .Add(pool.CreateSystem<ShieldCollisionSystem>())
                .Add(pool.CreateSystem<ShieldCollisionEffectSystem>())
                .Add(pool.CreateSystem<TrailSystem>())

                // bonus
                .Add(pool.CreateSystem<ActivateBonusSystem>())
                .Add(pool.CreateSystem<SpeedBonusSystem>())

            // RelativePositionGO
            .Add(pool.CreateSystem<RelativePositionSystem>())

            // PositionGO
            .Add(pool.CreateSystem<PositionGameObjectSystem>())

            .Add(pool.CreateSystem<StartGameSystem>())

            // RemoveGO
            .Add(pool.CreateSystem<RemoveGameObjectSystem>())

            // Camera
            .Add(pool.CreateSystem<DefaultCameraSystem>())
            .Add(pool.CreateSystem<SmoothCameraSystem>())

            // Stats
            .Add(pool.CreateSystem<GameStatsSystem>())

            // Sound
            .Add(pool.CreateSystem<SoundSystem>())

            // Destroy
            .Add(pool.CreateSystem<DestroyEntitySystem>())

            .Add(pool.CreateSystem<PauseGameSystem>())
            .Add(pool.CreateSystem<EndGameSystem>())
            .Add(pool.CreateSystem<SlowGameSystem>())
            .Add(pool.CreateSystem<CallOnFrameEndSystem>())
            .Add(pool.CreateSystem<DestroyEntityDelayedSystem>());
    }
}
