public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Camera = 1;
    public const int CircleMissileSpawner = 2;
    public const int Collision = 3;
    public const int CreateLevel = 4;
    public const int Damage = 5;
    public const int DestroyEntity = 6;
    public const int DestroyPosition = 7;
    public const int Enemy = 8;
    public const int EnemySpawner = 9;
    public const int FaceDirection = 10;
    public const int GameObject = 11;
    public const int Health = 12;
    public const int HomeMissile = 13;
    public const int HomeMissileSpawner = 14;
    public const int Input = 15;
    public const int LevelDimensions = 16;
    public const int MissileSpawner = 17;
    public const int MouseInput = 18;
    public const int Player = 19;
    public const int PoolableGO = 20;
    public const int Position = 21;
    public const int RegularCamera = 22;
    public const int Resource = 23;
    public const int RestartGame = 24;
    public const int SmoothCamera = 25;
    public const int SnapPosition = 26;
    public const int Test = 27;
    public const int Time = 28;
    public const int Velocity = 29;
    public const int VelocityLimit = 30;
    public const int Weapon = 31;

    public const int TotalComponents = 32;

    static readonly string[] components = {
        "Acceleration",
        "Camera",
        "CircleMissileSpawner",
        "Collision",
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
        "LevelDimensions",
        "MissileSpawner",
        "MouseInput",
        "Player",
        "PoolableGO",
        "Position",
        "RegularCamera",
        "Resource",
        "RestartGame",
        "SmoothCamera",
        "SnapPosition",
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