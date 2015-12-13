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
    public const int CameraShakeOnDeath = 10;
    public const int Canvas = 11;
    public const int Child = 12;
    public const int CircleMissileRotatedSpawner = 13;
    public const int CircleMissileSpawner = 14;
    public const int Collision = 15;
    public const int CollisionDeath = 16;
    public const int CreateCamera = 17;
    public const int CreateGrid = 18;
    public const int CreateLevel = 19;
    public const int CreatePlayer = 20;
    public const int CreateShip = 21;
    public const int CurrentShip = 22;
    public const int Damage = 23;
    public const int DefaultCamera = 24;
    public const int DelayedCall = 25;
    public const int DestroyEntity = 26;
    public const int DestroyEntityDelayed = 27;
    public const int DifficultyController = 28;
    public const int DifficultyModel = 29;
    public const int EndGame = 30;
    public const int Enemy = 31;
    public const int EnemySpawner = 32;
    public const int EventService = 33;
    public const int ExplosionOnDeath = 34;
    public const int FaceDirection = 35;
    public const int FindTarget = 36;
    public const int FirstBoss = 37;
    public const int FollowTarget = 38;
    public const int GameObject = 39;
    public const int GameStats = 40;
    public const int Ghost = 41;
    public const int Grid = 42;
    public const int GridField = 43;
    public const int Health = 44;
    public const int HomeMissile = 45;
    public const int HomeMissileSpawner = 46;
    public const int Indicator = 47;
    public const int IndicatorPanel = 48;
    public const int Input = 49;
    public const int Laser = 50;
    public const int LaserSpawner = 51;
    public const int LeaderFollower = 52;
    public const int LevelDimensions = 53;
    public const int LevelModel = 54;
    public const int Magnet = 55;
    public const int MaterialReference = 56;
    public const int MissileSpawner = 57;
    public const int MouseInput = 58;
    public const int MoveWithCamera = 59;
    public const int MovingBlockade = 60;
    public const int MultipleMissileSpawner = 61;
    public const int NonRemovable = 62;
    public const int Parent = 63;
    public const int ParticlesOnDeath = 64;
    public const int ParticleSpawn = 65;
    public const int Path = 66;
    public const int PathModel = 67;
    public const int PauseGame = 68;
    public const int Player = 69;
    public const int PlayerHealthBar = 70;
    public const int PlayerModel = 71;
    public const int PoolableGO = 72;
    public const int Position = 73;
    public const int RelativePosition = 74;
    public const int Resource = 75;
    public const int Rotate = 76;
    public const int SecondaryWeapon = 77;
    public const int SettingsModel = 78;
    public const int Shake = 79;
    public const int ShipModel = 80;
    public const int SlowGame = 81;
    public const int SmoothCamera = 82;
    public const int SnapPosition = 83;
    public const int SortOrder = 84;
    public const int Sound = 85;
    public const int SoundOnDeath = 86;
    public const int SpeedBonus = 87;
    public const int StartGame = 88;
    public const int StaticCamera = 89;
    public const int Test = 90;
    public const int Time = 91;
    public const int TweenPosition = 92;
    public const int UIFactoryService = 93;
    public const int UIResource = 94;
    public const int Velocity = 95;
    public const int VelocityLimit = 96;
    public const int WaveSpawner = 97;
    public const int Weapon = 98;

    public const int TotalComponents = 99;

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
        "CreateShip",
        "CurrentShip",
        "Damage",
        "DefaultCamera",
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
        "Ghost",
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
        "LeaderFollower",
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
        "RelativePosition",
        "Resource",
        "Rotate",
        "SecondaryWeapon",
        "SettingsModel",
        "Shake",
        "ShipModel",
        "SlowGame",
        "SmoothCamera",
        "SnapPosition",
        "SortOrder",
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
        typeof(CreateShipComponent),
        typeof(CurrentShipComponent),
        typeof(DamageComponent),
        typeof(DefaultCameraComponent),
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
        typeof(GhostComponent),
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
        typeof(LeaderFollowerComponent),
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
        typeof(RelativePositionComponent),
        typeof(ResourceComponent),
        typeof(RotateComponent),
        typeof(SecondaryWeaponComponent),
        typeof(SettingsModelComponent),
        typeof(ShakeComponent),
        typeof(ShipModelComponent),
        typeof(SlowGameComponent),
        typeof(SmoothCameraComponent),
        typeof(SnapPositionComponent),
        typeof(SortOrderComponent),
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