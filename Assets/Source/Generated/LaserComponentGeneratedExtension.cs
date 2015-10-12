using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public LaserComponent laser { get { return (LaserComponent)GetComponent(ComponentIds.Laser); } }

        public bool hasLaser { get { return HasComponent(ComponentIds.Laser); } }

        static readonly Stack<LaserComponent> _laserComponentPool = new Stack<LaserComponent>();

        public static void ClearLaserComponentPool() {
            _laserComponentPool.Clear();
        }

        public Entity AddLaser(float newHeight, Entitas.Entity newSource) {
            var component = _laserComponentPool.Count > 0 ? _laserComponentPool.Pop() : new LaserComponent();
            component.height = newHeight;
            component.source = newSource;
            return AddComponent(ComponentIds.Laser, component);
        }

        public Entity ReplaceLaser(float newHeight, Entitas.Entity newSource) {
            var previousComponent = hasLaser ? laser : null;
            var component = _laserComponentPool.Count > 0 ? _laserComponentPool.Pop() : new LaserComponent();
            component.height = newHeight;
            component.source = newSource;
            ReplaceComponent(ComponentIds.Laser, component);
            if (previousComponent != null) {
                _laserComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveLaser() {
            var component = laser;
            RemoveComponent(ComponentIds.Laser);
            _laserComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherLaser;

        public static AllOfMatcher Laser {
            get {
                if (_matcherLaser == null) {
                    _matcherLaser = new Matcher(ComponentIds.Laser);
                }

                return _matcherLaser;
            }
        }
    }
}