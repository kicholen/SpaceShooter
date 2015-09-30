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
    public const int GameObject = 9;
    public const int Health = 10;
    public const int Input = 11;
    public const int MissileSpawner = 12;
    public const int MouseInput = 13;
    public const int Player = 14;
    public const int Position = 15;
    public const int RegularCamera = 16;
    public const int Resource = 17;
    public const int SmoothCamera = 18;
    public const int Test = 19;
    public const int Velocity = 20;
    public const int VelocityLimit = 21;
    public const int Weapon = 22;

    public const int TotalComponents = 23;

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
        "GameObject",
        "Health",
        "Input",
        "MissileSpawner",
        "MouseInput",
        "Player",
        "Position",
        "RegularCamera",
        "Resource",
        "SmoothCamera",
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