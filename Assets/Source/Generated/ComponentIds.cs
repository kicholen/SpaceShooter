public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Bonus = 1;
    public const int BonusModel = 2;
    public const int BonusSpawner = 3;
    public const int Camera = 4;
    public const int CircleMissileSpawner = 5;
    public const int Collision = 6;
    public const int CollisionDeath = 7;
    public const int CreateLevel = 8;
    public const int Damage = 9;
    public const int DestroyEntity = 10;
    public const int DestroyPosition = 11;
    public const int Enemy = 12;
    public const int EnemySpawner = 13;
    public const int FaceDirection = 14;
    public const int GameObject = 15;
    public const int Health = 16;
    public const int HomeMissile = 17;
    public const int HomeMissileSpawner = 18;
    public const int Input = 19;
    public const int Laser = 20;
    public const int LaserSpawner = 21;
    public const int LevelDimensions = 22;
    public const int MissileSpawner = 23;
    public const int MouseInput = 24;
    public const int NonRemovable = 25;
    public const int Player = 26;
    public const int PoolableGO = 27;
    public const int Position = 28;
    public const int RegularCamera = 29;
    public const int Resource = 30;
    public const int RestartGame = 31;
    public const int SmoothCamera = 32;
    public const int SnapPosition = 33;
    public const int SpeedBonus = 34;
    public const int Test = 35;
    public const int Time = 36;
    public const int Velocity = 37;
    public const int VelocityLimit = 38;
    public const int Weapon = 39;

    public const int TotalComponents = 40;

    static readonly string[] components = {
        "Acceleration",
        "Bonus",
        "BonusModel",
        "BonusSpawner",
        "Camera",
        "CircleMissileSpawner",
        "Collision",
        "CollisionDeath",
        "CreateLevel",
        "Damage",
        "DestroyEntity",
        "DestroyPosition",
        "Enemy",
        "EnemySpawner",
        "FaceDirection",
        "GameObject",
        "Health",
        "HomeMissile",
        "HomeMissileSpawner",
        "Input",
        "Laser",
        "LaserSpawner",
        "LevelDimensions",
        "MissileSpawner",
        "MouseInput",
        "NonRemovable",
        "Player",
        "PoolableGO",
        "Position",
        "RegularCamera",
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