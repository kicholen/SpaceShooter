public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Active = 1;
    public const int Alpha = 2;
    public const int Bonus = 3;
    public const int BonusModel = 4;
    public const int BonusOnDeath = 5;
    public const int Camera = 6;
    public const int CameraShake = 7;
    public const int CameraShakeOnDeath = 8;
    public const int Child = 9;
    public const int CircleMissileRotatedSpawner = 10;
    public const int CircleMissileSpawner = 11;
    public const int Collision = 12;
    public const int CollisionDeath = 13;
    public const int CreateCamera = 14;
    public const int CreateLevel = 15;
    public const int CreatePlayer = 16;
    public const int Damage = 17;
    public const int DestroyEntity = 18;
    public const int DestroyEntityDelayed = 19;
    public const int DifficultyController = 20;
    public const int DifficultyModel = 21;
    public const int EndGame = 22;
    public const int Enemy = 23;
    public const int EnemySpawner = 24;
    public const int ExplosionOnDeath = 25;
    public const int FaceDirection = 26;
    public const int FindTarget = 27;
    public const int FirstBoss = 28;
    public const int FollowTarget = 29;
    public const int GameObject = 30;
    public const int GameStats = 31;
    public const int Health = 32;
    public const int HomeMissile = 33;
    public const int HomeMissileSpawner = 34;
    public const int Input = 35;
    public const int Laser = 36;
    public const int LaserSpawner = 37;
    public const int LevelDimensions = 38;
    public const int Magnet = 39;
    public const int MissileSpawner = 40;
    public const int MouseInput = 41;
    public const int MoveWithCamera = 42;
    public const int MultipleMissileSpawner = 43;
    public const int NonRemovable = 44;
    public const int Parent = 45;
    public const int ParticlesOnDeath = 46;
    public const int ParticleSpawn = 47;
    public const int Path = 48;
    public const int PathModel = 49;
    public const int PauseGame = 50;
    public const int Player = 51;
    public const int PlayerModel = 52;
    public const int PoolableGO = 53;
    public const int Position = 54;
    public const int RegularCamera = 55;
    public const int RelativePosition = 56;
    public const int Resource = 57;
    public const int SecondaryWeapon = 58;
    public const int SettingsModel = 59;
    public const int ShipModel = 60;
    public const int SlowGame = 61;
    public const int SmoothCamera = 62;
    public const int SnapPosition = 63;
    public const int Sound = 64;
    public const int SoundOnDeath = 65;
    public const int SpeedBonus = 66;
    public const int StartGame = 67;
    public const int StaticCamera = 68;
    public const int Test = 69;
    public const int Time = 70;
    public const int Velocity = 71;
    public const int VelocityLimit = 72;
    public const int WaveSpawner = 73;
    public const int Weapon = 74;

    public const int TotalComponents = 75;

    public static readonly string[] componentNames = {
        "Acceleration",
        "Active",
        "Alpha",
        "Bonus",
        "BonusModel",
        "BonusOnDeath",
        "Camera",
        "CameraShake",
        "CameraShakeOnDeath",
        "Child",
        "CircleMissileRotatedSpawner",
        "CircleMissileSpawner",
        "Collision",
        "CollisionDeath",
        "CreateCamera",
        "CreateLevel",
        "CreatePlayer",
        "Damage",
        "DestroyEntity",
        "DestroyEntityDelayed",
        "DifficultyController",
        "DifficultyModel",
        "EndGame",
        "Enemy",
        "EnemySpawner",
        "ExplosionOnDeath",
        "FaceDirection",
        "FindTarget",
        "FirstBoss",
        "FollowTarget",
        "GameObject",
        "GameStats",
        "Health",
        "HomeMissile",
        "HomeMissileSpawner",
        "Input",
        "Laser",
        "LaserSpawner",
        "LevelDimensions",
        "Magnet",
        "MissileSpawner",
        "MouseInput",
        "MoveWithCamera",
        "MultipleMissileSpawner",
        "NonRemovable",
        "Parent",
        "ParticlesOnDeath",
        "ParticleSpawn",
        "Path",
        "PathModel",
        "PauseGame",
        "Player",
        "PlayerModel",
        "PoolableGO",
        "Position",
        "RegularCamera",
        "RelativePosition",
        "Resource",
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
        typeof(CameraComponent),
        typeof(CameraShakeComponent),
        typeof(CameraShakeOnDeathComponent),
        typeof(ChildComponent),
        typeof(CircleMissileRotatedSpawnerComponent),
        typeof(CircleMissileSpawnerComponent),
        typeof(CollisionComponent),
        typeof(CollisionDeathComponent),
        typeof(CreateCameraComponent),
        typeof(CreateLevelComponent),
        typeof(CreatePlayerComponent),
        typeof(DamageComponent),
        typeof(DestroyEntityComponent),
        typeof(DestroyEntityDelayedComponent),
        typeof(DifficultyControllerComponent),
        typeof(DifficultyModelComponent),
        typeof(EndGameComponent),
        typeof(EnemyComponent),
        typeof(EnemySpawnerComponent),
        typeof(ExplosionOnDeathComponent),
        typeof(FaceDirectionComponent),
        typeof(FindTargetComponent),
        typeof(FirstBossComponent),
        typeof(FollowTargetComponent),
        typeof(GameObjectComponent),
        typeof(GameStatsComponent),
        typeof(HealthComponent),
        typeof(HomeMissileComponent),
        typeof(HomeMissileSpawnerComponent),
        typeof(InputComponent),
        typeof(LaserComponent),
        typeof(LaserSpawnerComponent),
        typeof(LevelDimensionsComponent),
        typeof(MagnetComponent),
        typeof(MissileSpawnerComponent),
        typeof(MouseInputComponent),
        typeof(MoveWithCamera),
        typeof(MultipleMissileSpawnerComponent),
        typeof(NonRemovableComponent),
        typeof(ParentComponent),
        typeof(ParticlesOnDeathComponent),
        typeof(ParticleSpawnComponent),
        typeof(PathComponent),
        typeof(PathModelComponent),
        typeof(PauseGameComponent),
        typeof(PlayerComponent),
        typeof(PlayerModelComponent),
        typeof(PoolableGOComponent),
        typeof(PositionComponent),
        typeof(RegularCameraComponent),
        typeof(RelativePositionComponent),
        typeof(ResourceComponent),
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
        typeof(VelocityComponent),
        typeof(VelocityLimitComponent),
        typeof(WaveSpawnerComponent),
        typeof(WeaponComponent)
    };
}