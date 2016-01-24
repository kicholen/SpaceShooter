using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public RotateComponent rotate { get { return (RotateComponent)GetComponent(ComponentIds.Rotate); } }

        public bool hasRotate { get { return HasComponent(ComponentIds.Rotate); } }

        static readonly Stack<RotateComponent> _rotateComponentPool = new Stack<RotateComponent>();

        public static void ClearRotateComponentPool() {
            _rotateComponentPool.Clear();
        }

        public Entity AddRotate(float newAngle, float newRotateSpeed) {
            var component = _rotateComponentPool.Count > 0 ? _rotateComponentPool.Pop() : new RotateComponent();
            component.angle = newAngle;
            component.rotateSpeed = newRotateSpeed;
            return AddComponent(ComponentIds.Rotate, component);
        }

        public Entity ReplaceRotate(float newAngle, float newRotateSpeed) {
            var previousComponent = hasRotate ? rotate : null;
            var component = _rotateComponentPool.Count > 0 ? _rotateComponentPool.Pop() : new RotateComponent();
            component.angle = newAngle;
            component.rotateSpeed = newRotateSpeed;
            ReplaceComponent(ComponentIds.Rotate, component);
            if (previousComponent != null) {
                _rotateComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveRotate() {
            var component = rotate;
            RemoveComponent(ComponentIds.Rotate);
            _rotateComponentPool.Push(component);
            return this;
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
