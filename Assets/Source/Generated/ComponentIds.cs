public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Alpha = 1;
    public const int Bonus = 2;
    public const int BonusModel = 3;
    public const int BonusSpawner = 4;
    public const int Camera = 5;
    public const int Child = 6;
    public const int CircleMissileSpawner = 7;
    public const int Collision = 8;
    public const int CollisionDeath = 9;
    public const int CreateLevel = 10;
    public const int Damage = 11;
    public const int DestroyEntity = 12;
    public const int DestroyEntityDelayed = 13;
    public const int DestroyPosition = 14;
    public const int Enemy = 15;
    public const int EnemySpawner = 16;
    public const int FaceDirection = 17;
    public const int FindTarget = 18;
    public const int FollowTarget = 19;
    public const int GameObject = 20;
    public const int Health = 21;
    public const int HomeMissile = 22;
    public const int HomeMissileSpawner = 23;
    public const int Input = 24;
    public const int Laser = 25;
    public const int LaserSpawner = 26;
    public const int LevelDimensions = 27;
    public const int Magnet = 28;
    public const int MissileSpawner = 29;
    public const int MouseInput = 30;
    public const int NonRemovable = 31;
    public const int Parent = 32;
    public const int ParticleSpawn = 33;
    public const int PauseGame = 34;
    public const int Player = 35;
    public const int PoolableGO = 36;
    public const int Position = 37;
    public const int RegularCamera = 38;
    public const int RelativePosition = 39;
    public const int Resource = 40;
    public const int RestartGame = 41;
    public const int SmoothCamera = 42;
    public const int SnapPosition = 43;
    public const int SpeedBonus = 44;
    public const int Test = 45;
    public const int Time = 46;
    public const int Velocity = 47;
    public const int VelocityLimit = 48;
    public const int Weapon = 49;

    public const int TotalComponents = 50;

    static readonly string[] components = {
        "Acceleration",
        "Alpha",
        "Bonus",
        "BonusModel",
        "BonusSpawner",
        "Camera",
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