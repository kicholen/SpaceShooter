public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Camera = 1;
    public const int Collision = 2;
    public const int CreateLevel = 3;
    public const int Damage = 4;
    public const int DestroyEntity = 5;
    public const int DestroyPosition = 6;
    public const int Enemy = 7;
    public const int EnemySpawner = 8;
    public const int FaceDirection = 9;
    public const int GameObject = 10;
    public const int Health = 11;
    public const int Input = 12;
    public const int LevelDimensions = 13;
    public const int MissileSpawner = 14;
    public const int MouseInput = 15;
    public const int Player = 16;
    public const int PoolableGO = 17;
    public const int Position = 18;
    public const int RegularCamera = 19;
    public const int Resource = 20;
    public const int RestartGame = 21;
    public const int SmoothCamera = 22;
    public const int SnapPosition = 23;
    public const int Test = 24;
    public const int Time = 25;
    public const int Velocity = 26;
    public const int VelocityLimit = 27;
    public const int Weapon = 28;

    public const int TotalComponents = 29;

    static readonly string[] components = {
        "Acceleration",
        "Camera",
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