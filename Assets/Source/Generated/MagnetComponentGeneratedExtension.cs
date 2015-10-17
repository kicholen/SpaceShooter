using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MagnetComponent magnet { get { return (MagnetComponent)GetComponent(ComponentIds.Magnet); } }

        public bool hasMagnet { get { return HasComponent(ComponentIds.Magnet); } }

        static readonly Stack<MagnetComponent> _magnetComponentPool = new Stack<MagnetComponent>();

        public static void ClearMagnetComponentPool() {
            _magnetComponentPool.Clear();
        }

        public Entity AddMagnet(float newVelocity, float newRadius) {
            var component = _magnetComponentPool.Count > 0 ? _magnetComponentPool.Pop() : new MagnetComponent();
            component.velocity = newVelocity;
            component.radius = newRadius;
            return AddComponent(ComponentIds.Magnet, component);
        }

        public Entity ReplaceMagnet(float newVelocity, float newRadius) {
            var previousComponent = hasMagnet ? magnet : null;
            var component = _magnetComponentPool.Count > 0 ? _magnetComponentPool.Pop() : new MagnetComponent();
            component.velocity = newVelocity;
            component.radius = newRadius;
            ReplaceComponent(ComponentIds.Magnet, component);
            if (previousComponent != null) {
                _magnetComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMagnet() {
            var component = magnet;
            RemoveComponent(ComponentIds.Magnet);
            _magnetComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherMagnet;

        public static AllOfMatcher Magnet {
            get {
                if (_matcherMagnet == null) {
                    _matcherMagnet = new Matcher(ComponentIds.Magnet);
                }

                return _matcherMagnet;
            }
        }
    }
}
