public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Alpha = 1;
    public const int Bonus = 2;
    public const int BonusModel = 3;
    public const int BonusOnDeath = 4;
    public const int Camera = 5;
    public const int CameraShake = 6;
    public const int CameraShakeOnDeath = 7;
    public const int Child = 8;
    public const int CircleMissileRotatedSpawner = 9;
    public const int CircleMissileSpawner = 10;
    public const int Collision = 11;
    public const int CollisionDeath = 12;
    public const int CreateLevel = 13;
    public const int CreatePlayer = 14;
    public const int Damage = 15;
    public const int DestroyEntity = 16;
    public const int DestroyEntityDelayed = 17;
    public const int DestroyPosition = 18;
    public const int DifficultyController = 19;
    public const int DifficultyModel = 20;
    public const int Enemy = 21;
    public const int EnemySpawner = 22;
    public const int FaceDirection = 23;
    public const int FindTarget = 24;
    public const int FirstBoss = 25;
    public const int FollowTarget = 26;
    public const int GameObject = 27;
    public const int Health = 28;
    public const int HomeMissile = 29;
    public const int HomeMissileSpawner = 30;
    public const int Input = 31;
    public const int Laser = 32;
    public const int LaserSpawner = 33;
    public const int LevelDimensions = 34;
    public const int Magnet = 35;
    public const int MissileSpawner = 36;
    public const int MouseInput = 37;
    public const int MultipleMissileSpawner = 38;
    public const int NonRemovable = 39;
    public const int Parent = 40;
    public const int ParticlesOnDeath = 41;
    public const int ParticleSpawn = 42;
    public const int PauseGame = 43;
    public const int Player = 44;
    public const int PlayerModel = 45;
    public const int PoolableGO = 46;
    public const int Position = 47;
    public const int RegularCamera = 48;
    public const int RelativePosition = 49;
    public const int Resource = 50;
    public const int RestartGame = 51;
    public const int SettingsModel = 52;
    public const int ShipModel = 53;
    public const int SlowGame = 54;
    public const int SmoothCamera = 55;
    public const int SnapPosition = 56;
    public const int Sound = 57;
    public const int SpeedBonus = 58;
    public const int Test = 59;
    public const int Time = 60;
    public const int Velocity = 61;
    public const int VelocityLimit = 62;
    public const int Weapon = 63;

    public const int TotalComponents = 64;

    static readonly string[] components = {
        "Acceleration",
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
        "DestroyPosition",
        "DifficultyController",
        "DifficultyModel",
        "Enemy",
        "EnemySpawner",
        "FaceDirection",
        "FindTarget",
        "FirstBoss",
        "FollowTarget",
        "GameObject",
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
        "MultipleMissileSpawner",
        "NonRemovable",
        "Parent",
        "ParticlesOnDeath",
        "ParticleSpawn",
        "PauseGame",
        "Player",
        "PlayerModel",
        "PoolableGO",
        "Position",
        "RegularCamera",
        "RelativePosition",
        "Resource",
        "RestartGame",
        "SettingsModel",
        "ShipModel",
        "SlowGame",
        "SmoothCamera",
        "SnapPosition",
        "Sound",
        "SpeedBonus",
        "Test",
        "Time",
        "Velocity",
        "VelocityLimit",
        "Weapon"
    };

    public static string IdToString(int componentId) {
        return components[componentId];
    }
}

namespace Entitas {
    public partial class Matcher : AllOfMatcher {
        public Matcher(int index) : base(new [] { index }) {
        }

        public override string ToString() {
            return ComponentIds.IdToString(indices[0]);
        }
    }
}