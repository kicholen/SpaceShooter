public static class ComponentIds {
    public const int Acceleration = 0;
    public const int Camera = 1;
    public const int Collision = 2;
    public const int DestroyEntity = 3;
    public const int DestroyPosition = 4;
    public const int GameObject = 5;
    public const int Input = 6;
    public const int MissileSpawner = 7;
    public const int MouseInput = 8;
    public const int Player = 9;
    public const int Position = 10;
    public const int Resource = 11;
    public const int Velocity = 12;
    public const int VelocityLimit = 13;
    public const int Weapon = 14;

    public const int TotalComponents = 15;

    static readonly string[] components = {
        "Acceleration",
        "Camera",
        "Collision",
        "DestroyEntity",
        "DestroyPosition",
        "GameObject",
        "Input",
        "MissileSpawner",
        "MouseInput",
        "Player",
        "Position",
        "Resource",
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