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
    public const int CreateLevel = 14;
    public const int CreatePlayer = 15;
    public const int Damage = 16;
    public const int DestroyEntity = 17;
    public const int DestroyEntityDelayed = 18;
    public const int DifficultyController = 19;
    public const int DifficultyModel = 20;
    public const int Enemy = 21;
    public const int EnemySpawner = 22;
    public const int FaceDirection = 23;
    public const int FindTarget = 24;
    public const int FirstBoss = 25;
    public const int FollowTarget = 26;
    public const int GameObject = 27;
    public const int GameStats = 28;
    public const int Health = 29;
    public const int HomeMissile = 30;
    public const int HomeMissileSpawner = 31;
    public const int Input = 32;
    public const int Laser = 33;
    public const int LaserSpawner = 34;
    public const int LevelDimensions = 35;
    public const int Magnet = 36;
    public const int MissileSpawner = 37;
    public const int MouseInput = 38;
    public const int MoveWithCamera = 39;
    public const int MultipleMissileSpawner = 40;
    public const int NonRemovable = 41;
    public const int Parent = 42;
    public const int ParticlesOnDeath = 43;
    public const int ParticleSpawn = 44;
    public const int Path = 45;
    public const int PathModel = 46;
    public const int PauseGame = 47;
    public const int Player = 48;
    public const int PlayerModel = 49;
    public const int PoolableGO = 50;
    public const int Position = 51;
    public const int RegularCamera = 52;
    public const int RelativePosition = 53;
    public const int Resource = 54;
    public const int RestartGame = 55;
    public const int SecondaryWeapon = 56;
    public const int SettingsModel = 57;
    public const int ShipModel = 58;
    public const int SlowGame = 59;
    public const int SmoothCamera = 60;
    public const int SnapPosition = 61;
    public const int Sound = 62;
    public const int SoundOnDeath = 63;
    public const int SpeedBonus = 64;
    public const int Test = 65;
    public const int Time = 66;
    public const int Velocity = 67;
    public const int VelocityLimit = 68;
    public const int WaveSpawner = 69;
    public const int Weapon = 70;

    public const int TotalComponents = 71;

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
        "CreateLevel",
        "CreatePlayer",
        "Damage",
        "DestroyEntity",
        "DestroyEntityDelayed",
        "DifficultyController",
        "DifficultyModel",
        "Enemy",
        "EnemySpawner",
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
        "RestartGame",
        "SecondaryWeapon",
        "SettingsModel",
        "ShipModel",
        "SlowGame",
        "SmoothCamera",
        "SnapPosition",
        "Sound",
        "SoundOnDeath",
        "SpeedBonus",
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
        typeof(CreateLevelComponent),
        typeof(CreatePlayerComponent),
        typeof(DamageComponent),
        typeof(DestroyEntityComponent),
        typeof(DestroyEntityDelayedComponent),
        typeof(DifficultyControllerComponent),
        typeof(DifficultyModelComponent),
        typeof(EnemyComponent),
        typeof(EnemySpawnerComponent),
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
        typeof(RestartGameComponent),
        typeof(SecondaryWeaponComponent),
        typeof(SettingsModelComponent),
        typeof(ShipModelComponent),
        typeof(SlowGameComponent),
        typeof(SmoothCameraComponent),
        typeof(SnapPositionComponent),
        typeof(SoundComponent),
        typeof(SoundOnDeathComponent),
        typeof(SpeedBonusComponent),
        typeof(TestComponent),
        typeof(TimeComponent),
        typeof(VelocityComponent),
        typeof(VelocityLimitComponent),
        typeof(WaveSpawnerComponent),
        typeof(WeaponComponent)
    };
}