public static class ComponentIds {
    public const int DestroyEntity = 0;
    public const int GameObject = 1;
    public const int Input = 2;
    public const int Player = 3;
    public const int Position = 4;
    public const int Resource = 5;

    public const int TotalComponents = 6;

    static readonly string[] components = {
        "DestroyEntity",
        "GameObject",
        "Input",
        "Player",
        "Position",
        "Resource"
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