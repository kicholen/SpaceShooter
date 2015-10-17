public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Alpha = 1;
    public const int Bonus = 2;
    public const int BonusModel = 3;
    public const int BonusSpawner = 4;
    public const int Camera = 5;
    public const int CameraShake = 6;
    public const int Child = 7;
    public const int CircleMissileSpawner = 8;
    public const int Collision = 9;
    public const int CollisionDeath = 10;
    public const int CreateLevel = 11;
    public const int Damage = 12;
    public const int DestroyEntity = 13;
    public const int DestroyEntityDelayed = 14;
    public const int DestroyPosition = 15;
    public const int Enemy = 16;
    public const int EnemySpawner = 17;
    public const int FaceDirection = 18;
    public const int FindTarget = 19;
    public const int FollowTarget = 20;
    public const int GameObject = 21;
    public const int Health = 22;
    public const int HomeMissile = 23;
    public const int HomeMissileSpawner = 24;
    public const int Input = 25;
    public const int Laser = 26;
    public const int LaserSpawner = 27;
    public const int LevelDimensions = 28;
    public const int Magnet = 29;
    public const int MissileSpawner = 30;
    public const int MouseInput = 31;
    public const int NonRemovable = 32;
    public const int Parent = 33;
    public const int ParticleSpawn = 34;
    public const int PauseGame = 35;
    public const int Player = 36;
    public const int PoolableGO = 37;
    public const int Position = 38;
    public const int RegularCamera = 39;
    public const int RelativePosition = 40;
    public const int Resource = 41;
    public const int RestartGame = 42;
    public const int SmoothCamera = 43;
    public const int SnapPosition = 44;
    public const int SpeedBonus = 45;
    public const int Test = 46;
    public const int Time = 47;
    public const int Velocity = 48;
    public const int VelocityLimit = 49;
    public const int Weapon = 50;

    public const int TotalComponents = 51;

    static readonly string[] components = {
        "Acceleration",
        "Alpha",
        "Bonus",
        "BonusModel",
        "BonusSpawner",
        "Camera",
        "CameraShake",
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
        "ParticleSpawn",
        "PauseGame",
        "Player",
        "PoolableGO",
        "Position",
        "RegularCamera",
        "RelativePosition",
        "Resource",
        "RestartGame",
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