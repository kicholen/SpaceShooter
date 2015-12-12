using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ShakeComponent shake { get { return (ShakeComponent)GetComponent(ComponentIds.Shake); } }

        public bool hasShake { get { return HasComponent(ComponentIds.Shake); } }

        static readonly Stack<ShakeComponent> _shakeComponentPool = new Stack<ShakeComponent>();

        public static void ClearShakeComponentPool() {
            _shakeComponentPool.Clear();
        }

        public Entity AddShake(ShakeProperties newProperties) {
            var component = _shakeComponentPool.Count > 0 ? _shakeComponentPool.Pop() : new ShakeComponent();
            component.properties = newProperties;
            return AddComponent(ComponentIds.Shake, component);
        }

        public Entity ReplaceShake(ShakeProperties newProperties) {
            var previousComponent = hasShake ? shake : null;
            var component = _shakeComponentPool.Count > 0 ? _shakeComponentPool.Pop() : new ShakeComponent();
            component.properties = newProperties;
            ReplaceComponent(ComponentIds.Shake, component);
            if (previousComponent != null) {
                _shakeComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveShake() {
            var component = shake;
            RemoveComponent(ComponentIds.Shake);
            _shakeComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherShake;

        public static IMatcher Shake {
            get {
                if (_matcherShake == null) {
                    _matcherShake = Matcher.AllOf(ComponentIds.Shake);
                }

                return _matcherShake;
            }
        }
    }
}
