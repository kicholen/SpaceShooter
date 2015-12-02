public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Active = 1;
    public const int Alpha = 2;
    public const int Background = 3;
    public const int BackgroundStar = 4;
    public const int Bonus = 5;
    public const int BonusModel = 6;
    public const int BonusOnDeath = 7;
    public const int CallOnFrameEnd = 8;
    public const int Camera = 9;
    public const int CameraShake = 10;
    public const int CameraShakeOnDeath = 11;
    public const int Canvas = 12;
    public const int Child = 13;
    public const int CircleMissileRotatedSpawner = 14;
    public const int CircleMissileSpawner = 15;
    public const int Collision = 16;
    public const int CollisionDeath = 17;
    public const int CreateCamera = 18;
    public const int CreateGrid = 19;
    public const int CreateLevel = 20;
    public const int CreatePlayer = 21;
    public const int Damage = 22;
    public const int DelayedCall = 23;
    public const int DestroyEntity = 24;
    public const int DestroyEntityDelayed = 25;
    public const int DifficultyController = 26;
    public const int DifficultyModel = 27;
    public const int EndGame = 28;
    public const int Enemy = 29;
    public const int EnemySpawner = 30;
    public const int EventService = 31;
    public const int ExplosionOnDeath = 32;
    public const int FaceDirection = 33;
    public const int FindTarget = 34;
    public const int FirstBoss = 35;
    public const int FollowTarget = 36;
    public const int GameObject = 37;
    public const int GameStats = 38;
    public const int Grid = 39;
    public const int GridField = 40;
    public const int Health = 41;
    public const int HomeMissile = 42;
    public const int HomeMissileSpawner = 43;
    public const int Indicator = 44;
    public const int IndicatorPanel = 45;
    public const int Input = 46;
    public const int Laser = 47;
    public const int LaserSpawner = 48;
    public const int LevelDimensions = 49;
    public const int LevelModel = 50;
    public const int Magnet = 51;
    public const int MaterialReference = 52;
    public const int MissileSpawner = 53;
    public const int MouseInput = 54;
    public const int MoveWithCamera = 55;
    public const int MovingBlockade = 56;
    public const int MultipleMissileSpawner = 57;
    public const int NonRemovable = 58;
    public const int Parent = 59;
    public const int ParticlesOnDeath = 60;
    public const int ParticleSpawn = 61;
    public const int Path = 62;
    public const int PathModel = 63;
    public const int PauseGame = 64;
    public const int Player = 65;
    public const int PlayerHealthBar = 66;
    public const int PlayerModel = 67;
    public const int PoolableGO = 68;
    public const int Position = 69;
    public const int RegularCamera = 70;
    public const int RelativePosition = 71;
    public const int Resource = 72;
    public const int Rotate = 73;
    public const int SecondaryWeapon = 74;
    public const int SettingsModel = 75;
    public const int ShipModel = 76;
    public const int SlowGame = 77;
    public const int SmoothCamera = 78;
    public const int SnapPosition = 79;
    public const int Sound = 80;
    public const int SoundOnDeath = 81;
    public const int SpeedBonus = 82;
    public const int StartGame = 83;
    public const int StaticCamera = 84;
    public const int Test = 85;
    public const int Time = 86;
    public const int TweenPosition = 87;
    public const int UIFactoryService = 88;
    public const int UIResource = 89;
    public const int Velocity = 90;
    public const int VelocityLimit = 91;
    public const int WaveSpawner = 92;
    public const int Weapon = 93;

    public const int TotalComponents = 94;

    public static readonly string[] componentNames = {
        "Acceleration",
        "Active",
        "Alpha",
        "Background",
        "BackgroundStar",
        "Bonus",
        "BonusModel",
        "BonusOnDeath",
        "CallOnFrameEnd",
        "Camera",
        "CameraShake",
        "CameraShakeOnDeath",
        "Canvas",
        "Child",
        "CircleMissileRotatedSpawner",
        "CircleMissileSpawner",
        "Collision",
        "CollisionDeath",
        "CreateCamera",
        "CreateGrid",
        "CreateLevel",
        "CreatePlayer",
        "Damage",
        "DelayedCall",
        "DestroyEntity",
        "DestroyEntityDelayed",
        "DifficultyController",
        "DifficultyModel",
        "EndGame",
        "Enemy",
        "EnemySpawner",
        "EventService",
        "ExplosionOnDeath",
        "FaceDirection",
        "FindTarget",
        "FirstBoss",
        "FollowTarget",
        "GameObject",
        "GameStats",
        "Grid",
        "GridField",
        "Health",
        "HomeMissile",
        "HomeMissileSpawner",
        "Indicator",
        "IndicatorPanel",
        "Input",
        "Laser",
        "LaserSpawner",
        "LevelDimensions",
        "LevelModel",
        "Magnet",
        "MaterialReference",
        "MissileSpawner",
        "MouseInput",
        "MoveWithCamera",
        "MovingBlockade",
        "MultipleMissileSpawner",
        "NonRemovable",
        "Parent",
        "ParticlesOnDeath",
        "ParticleSpawn",
        "Path",
        "PathModel",
        "PauseGame",
        "Player",
        "PlayerHealthBar",
        "PlayerModel",
        "PoolableGO",
        "Position",
        "RegularCamera",
        "RelativePosition",
        "Resource",
        "Rotate",
        "SecondaryWeapon",
        "SettingsModel",
        "ShipModel",
        "SlowGame",
        "SmoothCamera",
        "SnapPosition",
        "Sound",
        "SoundOnDeath",
        "SpeedBonus",
        "StartGame",
        "StaticCamera",
        "Test",
        "Time",
        "TweenPosition",
        "UIFactoryService",
        "UIResource",
        "Velocity",
        "VelocityLimit",
        "WaveSpawner",
        "Weapon"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(AccelerationComponent),
        typeof(ActiveComponent),
        typeof(AlphaComponent),
        typeof(BackgroundComponent),
        typeof(BackgroundStarComponent),
        typeof(BonusComponent),
        typeof(BonusModelComponent),
        typeof(BonusOnDeathComponent),
        typeof(CallOnFrameEndComponent),
        typeof(CameraComponent),
        typeof(CameraShakeComponent),
        typeof(CameraShakeOnDeathComponent),
        typeof(CanvasComponent),
        typeof(ChildComponent),
        typeof(CircleMissileRotatedSpawnerComponent),
        typeof(CircleMissileSpawnerComponent),
        typeof(CollisionComponent),
        typeof(CollisionDeathComponent),
        typeof(CreateCameraComponent),
        typeof(CreateGridComponent),
        typeof(CreateLevelComponent),
        typeof(CreatePlayerComponent),
        typeof(DamageComponent),
        typeof(DelayedCallComponent),
        typeof(DestroyEntityComponent),
        typeof(DestroyEntityDelayedComponent),
        typeof(DifficultyControllerComponent),
        typeof(DifficultyModelComponent),
        typeof(EndGameComponent),
        typeof(EnemyComponent),
        typeof(EnemySpawnerComponent),
        typeof(EventServiceComponent),
        typeof(ExplosionOnDeathComponent),
        typeof(FaceDirectionComponent),
        typeof(FindTargetComponent),
        typeof(FirstBossComponent),
        typeof(FollowTargetComponent),
        typeof(GameObjectComponent),
        typeof(GameStatsComponent),
        typeof(GridComponent),
        typeof(GridFieldComponent),
        typeof(HealthComponent),
        typeof(HomeMissileComponent),
        typeof(HomeMissileSpawnerComponent),
        typeof(IndicatorComponent),
        typeof(IndicatorPanelComponent),
        typeof(InputComponent),
        typeof(LaserComponent),
        typeof(LaserSpawnerComponent),
        typeof(LevelDimensionsComponent),
        typeof(LevelModelComponent),
        typeof(MagnetComponent),
        typeof(MaterialReferenceComponent),
        typeof(MissileSpawnerComponent),
        typeof(MouseInputComponent),
        typeof(MoveWithCamera),
        typeof(MovingBlockadeComponent),
        typeof(MultipleMissileSpawnerComponent),
        typeof(NonRemovableComponent),
        typeof(ParentComponent),
        typeof(ParticlesOnDeathComponent),
        typeof(ParticleSpawnComponent),
        typeof(PathComponent),
        typeof(PathModelComponent),
        typeof(PauseGameComponent),
        typeof(PlayerComponent),
        typeof(PlayerHealthBarComponent),
        typeof(PlayerModelComponent),
        typeof(PoolableGOComponent),
        typeof(PositionComponent),
        typeof(RegularCameraComponent),
        typeof(RelativePositionComponent),
        typeof(ResourceComponent),
        typeof(RotateComponent),
        typeof(SecondaryWeaponComponent),
        typeof(SettingsModelComponent),
        typeof(ShipModelComponent),
        typeof(SlowGameComponent),
        typeof(SmoothCameraComponent),
        typeof(SnapPositionComponent),
        typeof(SoundComponent),
        typeof(SoundOnDeathComponent),
        typeof(SpeedBonusComponent),
        typeof(StartGameComponent),
        typeof(StaticCameraComponent),
        typeof(TestComponent),
        typeof(TimeComponent),
        typeof(TweenPositionComponent),
        typeof(UIFactoryServiceComponent),
        typeof(UIResourceComponent),
        typeof(VelocityComponent),
        typeof(VelocityLimitComponent),
        typeof(WaveSpawnerComponent),
        typeof(WeaponComponent)
    };
}