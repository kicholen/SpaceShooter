//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {
    public partial class Entity {
        public TimeComponent time { get { return (TimeComponent)GetComponent(ComponentIds.Time); } }

        public bool hasTime { get { return HasComponent(ComponentIds.Time); } }

        public Entity AddTime(float newDeltaTime, float newGameDeltaTime, float newTime, float newModificator, bool newIsPaused) {
            var component = CreateComponent<TimeComponent>(ComponentIds.Time);
            component.deltaTime = newDeltaTime;
            component.gameDeltaTime = newGameDeltaTime;
            component.time = newTime;
            component.modificator = newModificator;
            component.isPaused = newIsPaused;
            return AddComponent(ComponentIds.Time, component);
        }

        public Entity ReplaceTime(float newDeltaTime, float newGameDeltaTime, float newTime, float newModificator, bool newIsPaused) {
            var component = CreateComponent<TimeComponent>(ComponentIds.Time);
            component.deltaTime = newDeltaTime;
            component.gameDeltaTime = newGameDeltaTime;
            component.time = newTime;
            component.modificator = newModificator;
            component.isPaused = newIsPaused;
            ReplaceComponent(ComponentIds.Time, component);
            return this;
        }

        public Entity RemoveTime() {
            return RemoveComponent(ComponentIds.Time);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTime;

        public static IMatcher Time {
            get {
                if (_matcherTime == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Time);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTime = matcher;
                }

                return _matcherTime;
            }
        }
    }
}
