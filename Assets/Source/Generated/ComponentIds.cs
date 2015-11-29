public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Active = 1;
    public const int Alpha = 2;
    public const int Bonus = 3;
    public const int BonusModel = 4;
    public const int BonusOnDeath = 5;
    public const int CallOnFrameEnd = 6;
    public const int Camera = 7;
    public const int CameraShake = 8;
    public const int CameraShakeOnDeath = 9;
    public const int Canvas = 10;
    public const int Child = 11;
    public const int CircleMissileRotatedSpawner = 12;
    public const int CircleMissileSpawner = 13;
    public const int Collision = 14;
    public const int CollisionDeath = 15;
    public const int CreateCamera = 16;
    public const int CreateGrid = 17;
    public const int CreateLevel = 18;
    public const int CreatePlayer = 19;
    public const int Damage = 20;
    public const int DelayedCall = 21;
    public const int DestroyEntity = 22;
    public const int DestroyEntityDelayed = 23;
    public const int DifficultyController = 24;
    public const int DifficultyModel = 25;
    public const int EndGame = 26;
    public const int Enemy = 27;
    public const int EnemySpawner = 28;
    public const int EventService = 29;
    public const int ExplosionOnDeath = 30;
    public const int FaceDirection = 31;
    public const int FindTarget = 32;
    public const int FirstBoss = 33;
    public const int FollowTarget = 34;
    public const int GameObject = 35;
    public const int GameStats = 36;
    public const int Grid = 37;
    public const int GridField = 38;
    public const int Health = 39;
    public const int HomeMissile = 40;
    public const int HomeMissileSpawner = 41;
    public const int Indicator = 42;
    public const int IndicatorPanel = 43;
    public const int Input = 44;
    public const int Laser = 45;
    public const int LaserSpawner = 46;
    public const int LevelDimensions = 47;
    public const int LevelModel = 48;
    public const int Magnet = 49;
    public const int MaterialReference = 50;
    public const int MissileSpawner = 51;
    public const int MouseInput = 52;
    public const int MoveWithCamera = 53;
    public const int MovingBlockade = 54;
    public const int MultipleMissileSpawner = 55;
    public const int NonRemovable = 56;
    public const int Parent = 57;
    public const int ParticlesOnDeath = 58;
    public const int ParticleSpawn = 59;
    public const int Path = 60;
    public const int PathModel = 61;
    public const int PauseGame = 62;
    public const int Player = 63;
    public const int PlayerHealthBar = 64;
    public const int PlayerModel = 65;
    public const int PoolableGO = 66;
    public const int Position = 67;
    public const int RegularCamera = 68;
    public const int RelativePosition = 69;
    public const int Resource = 70;
    public const int Rotate = 71;
    public const int SecondaryWeapon = 72;
    public const int SettingsModel = 73;
    public const int ShipModel = 74;
    public const int SlowGame = 75;
    public const int SmoothCamera = 76;
    public const int SnapPosition = 77;
    public const int Sound = 78;
    public const int SoundOnDeath = 79;
    public const int SpeedBonus = 80;
    public const int StartGame = 81;
    public const int StaticCamera = 82;
    public const int Test = 83;
    public const int Time = 84;
    public const int TweenPosition = 85;
    public const int UIFactoryService = 86;
    public const int UIResource = 87;
    public const int Velocity = 88;
    public const int VelocityLimit = 89;
    public const int WaveSpawner = 90;
    public const int Weapon = 91;

    public const int TotalComponents = 92;

    public static readonly string[] componentNames = {
        "Acceleration",
        "Active",
        "Alpha",
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