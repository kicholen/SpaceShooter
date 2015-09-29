public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Camera = 1;
    public const int Collision = 2;
    public const int Damage = 3;
    public const int DestroyEntity = 4;
    public const int DestroyPosition = 5;
    public const int Enemy = 6;
    public const int GameObject = 7;
    public const int Health = 8;
    public const int Input = 9;
    public const int MissileSpawner = 10;
    public const int MouseInput = 11;
    public const int Player = 12;
    public const int Position = 13;
    public const int RegularCamera = 14;
    public const int Resource = 15;
    public const int SmoothCamera = 16;
    public const int Test = 17;
    public const int Velocity = 18;
    public const int VelocityLimit = 19;
    public const int Weapon = 20;

    public const int TotalComponents = 21;

    static readonly string[] components = {
        "Acceleration",
        "Camera",
        "Collision",
        "Damage",
        "DestroyEntity",
        "DestroyPosition",
        "Enemy",
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