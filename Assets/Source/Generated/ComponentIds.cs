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
    public const int CircleMissileSpawner = 9;
    public const int Collision = 10;
    public const int CollisionDeath = 11;
    public const int CreateLevel = 12;
    public const int Damage = 13;
    public const int DestroyEntity = 14;
    public const int DestroyEntityDelayed = 15;
    public const int DestroyPosition = 16;
    public const int Enemy = 17;
    public const int EnemySpawner = 18;
    public const int FaceDirection = 19;
    public const int FindTarget = 20;
    public const int FollowTarget = 21;
    public const int GameObject = 22;
    public const int Health = 23;
    public const int HomeMissile = 24;
    public const int HomeMissileSpawner = 25;
    public const int Input = 26;
    public const int Laser = 27;
    public const int LaserSpawner = 28;
    public const int LevelDimensions = 29;
    public const int Magnet = 30;
    public const int MissileSpawner = 31;
    public const int MouseInput = 32;
    public const int NonRemovable = 33;
    public const int Parent = 34;
    public const int ParticlesOnDeath = 35;
    public const int ParticleSpawn = 36;
    public const int PauseGame = 37;
    public const int Player = 38;
    public const int PoolableGO = 39;
    public const int Position = 40;
    public const int RegularCamera = 41;
    public const int RelativePosition = 42;
    public const int Resource = 43;
    public const int RestartGame = 44;
    public const int SlowGame = 45;
    public const int SmoothCamera = 46;
    public const int SnapPosition = 47;
    public const int SpeedBonus = 48;
    public const int Test = 49;
    public const int Time = 50;
    public const int Velocity = 51;
    public const int VelocityLimit = 52;
    public const int Weapon = 53;

    public const int TotalComponents = 54;

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
        "CircleMissileSpawner",
        "Collision",
        "CollisionDeath",
        "CreateLevel",
        "Damage",
        "DestroyEntity",
        "DestroyEntityDelayed",
        "DestroyPosition",
        "Enemy",
        "EnemySpawner",
        "FaceDirection",
        "FindTarget",
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
        "NonRemovable",
        "Parent",
        "ParticlesOnDeath",
        "ParticleSpawn",
        "PauseGame",
        "Player",
        "PoolableGO",
        "Position",
        "RegularCamera",
        "RelativePosition",
        "Resource",
        "RestartGame",
        "SlowGame",
        "SmoothCamera",
        "SnapPosition",
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