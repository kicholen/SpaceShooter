public static class ComponentIds {
    public const int Acceleration = 0;
    public const int DestroyEntity = 1;
    public const int GameObject = 2;
    public const int Input = 3;
    public const int MissileSpawner = 4;
    public const int MouseInput = 5;
    public const int Player = 6;
    public const int Position = 7;
    public const int Resource = 8;
    public const int Velocity = 9;

    public const int TotalComponents = 10;

    static readonly string[] components = {
        "Acceleration",
        "DestroyEntity",
        "GameObject",
        "Input",
        "MissileSpawner",
        "MouseInput",
        "Player",
        "Position",
        "Resource",
        "Velocity"
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