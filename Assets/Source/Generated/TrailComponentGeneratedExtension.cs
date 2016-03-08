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
        public TrailComponent trail { get { return (TrailComponent)GetComponent(ComponentIds.Trail); } }

        public bool hasTrail { get { return HasComponent(ComponentIds.Trail); } }

        public Entity AddTrail(float newTime) {
            var component = CreateComponent<TrailComponent>(ComponentIds.Trail);
            component.time = newTime;
            return AddComponent(ComponentIds.Trail, component);
        }

        public Entity ReplaceTrail(float newTime) {
            var component = CreateComponent<TrailComponent>(ComponentIds.Trail);
            component.time = newTime;
            ReplaceComponent(ComponentIds.Trail, component);
            return this;
        }

        public Entity RemoveTrail() {
            return RemoveComponent(ComponentIds.Trail);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTrail;

        public static IMatcher Trail {
            get {
                if (_matcherTrail == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Trail);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTrail = matcher;
                }

                return _matcherTrail;
            }
        }
    }
}
