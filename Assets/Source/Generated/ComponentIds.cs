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
    public const int Position = 17;
    public const int RegularCamera = 18;
    public const int Resource = 19;
    public const int SmoothCamera = 20;
    public const int SnapPosition = 21;
    public const int Test = 22;
    public const int Velocity = 23;
    public const int VelocityLimit = 24;
    public const int Weapon = 25;

    public const int TotalComponents = 26;

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
        "Position",
        "RegularCamera",
        "Resource",
        "SmoothCamera",
        "SnapPosition",
        "Test",
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