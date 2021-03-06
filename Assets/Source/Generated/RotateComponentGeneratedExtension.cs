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
        public RotateComponent rotate { get { return (RotateComponent)GetComponent(ComponentIds.Rotate); } }

        public bool hasRotate { get { return HasComponent(ComponentIds.Rotate); } }

        public Entity AddRotate(float newAngle, float newRotateSpeed) {
            var component = CreateComponent<RotateComponent>(ComponentIds.Rotate);
            component.angle = newAngle;
            component.rotateSpeed = newRotateSpeed;
            return AddComponent(ComponentIds.Rotate, component);
        }

        public Entity ReplaceRotate(float newAngle, float newRotateSpeed) {
            var component = CreateComponent<RotateComponent>(ComponentIds.Rotate);
            component.angle = newAngle;
            component.rotateSpeed = newRotateSpeed;
            ReplaceComponent(ComponentIds.Rotate, component);
            return this;
        }

        public Entity RemoveRotate() {
            return RemoveComponent(ComponentIds.Rotate);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherRotate;

        public static IMatcher Rotate {
            get {
                if (_matcherRotate == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Rotate);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherRotate = matcher;
                }

                return _matcherRotate;
            }
        }
    }
}
