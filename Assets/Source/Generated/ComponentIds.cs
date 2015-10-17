public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Bonus = 1;
    public const int BonusModel = 2;
    public const int BonusSpawner = 3;
    public const int Camera = 4;
    public const int Child = 5;
    public const int CircleMissileSpawner = 6;
    public const int Collision = 7;
    public const int CollisionDeath = 8;
    public const int CreateLevel = 9;
    public const int Damage = 10;
    public const int DestroyEntity = 11;
    public const int DestroyEntityDelayed = 12;
    public const int DestroyPosition = 13;
    public const int Enemy = 14;
    public const int EnemySpawner = 15;
    public const int FaceDirection = 16;
    public const int FindTarget = 17;
    public const int FollowTarget = 18;
    public const int GameObject = 19;
    public const int Health = 20;
    public const int HomeMissile = 21;
    public const int HomeMissileSpawner = 22;
    public const int Input = 23;
    public const int Laser = 24;
    public const int LaserSpawner = 25;
    public const int LevelDimensions = 26;
    public const int MissileSpawner = 27;
    public const int MouseInput = 28;
    public const int NonRemovable = 29;
    public const int Parent = 30;
    public const int Player = 31;
    public const int PoolableGO = 32;
    public const int Position = 33;
    public const int RegularCamera = 34;
    public const int RelativePosition = 35;
    public const int Resource = 36;
    public const int RestartGame = 37;
    public const int SmoothCamera = 38;
    public const int SnapPosition = 39;
    public const int SpeedBonus = 40;
    public const int Test = 41;
    public const int Time = 42;
    public const int Velocity = 43;
    public const int VelocityLimit = 44;
    public const int Weapon = 45;

    public const int TotalComponents = 46;

    static readonly string[] components = {
        "Acceleration",
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
        "MissileSpawner",
        "MouseInput",
        "NonRemovable",
        "Parent",
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