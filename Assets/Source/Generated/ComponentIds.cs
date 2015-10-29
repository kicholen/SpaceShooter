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
    public const int Enemy = 19;
    public const int EnemySpawner = 20;
    public const int FaceDirection = 21;
    public const int FindTarget = 22;
    public const int FirstBoss = 23;
    public const int FollowTarget = 24;
    public const int GameObject = 25;
    public const int Health = 26;
    public const int HomeMissile = 27;
    public const int HomeMissileSpawner = 28;
    public const int Input = 29;
    public const int Laser = 30;
    public const int LaserSpawner = 31;
    public const int LevelDimensions = 32;
    public const int Magnet = 33;
    public const int MissileSpawner = 34;
    public const int MouseInput = 35;
    public const int MultipleMissileSpawner = 36;
    public const int NonRemovable = 37;
    public const int Parent = 38;
    public const int ParticlesOnDeath = 39;
    public const int ParticleSpawn = 40;
    public const int PauseGame = 41;
    public const int Player = 42;
    public const int PlayerModel = 43;
    public const int PoolableGO = 44;
    public const int Position = 45;
    public const int RegularCamera = 46;
    public const int RelativePosition = 47;
    public const int Resource = 48;
    public const int RestartGame = 49;
    public const int ShipModel = 50;
    public const int SlowGame = 51;
    public const int SmoothCamera = 52;
    public const int SnapPosition = 53;
    public const int Sound = 54;
    public const int SpeedBonus = 55;
    public const int Test = 56;
    public const int Time = 57;
    public const int Velocity = 58;
    public const int VelocityLimit = 59;
    public const int Weapon = 60;

    public const int TotalComponents = 61;

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