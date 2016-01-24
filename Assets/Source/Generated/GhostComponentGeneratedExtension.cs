using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public GhostComponent ghost { get { return (GhostComponent)GetComponent(ComponentIds.Ghost); } }

        public bool hasGhost { get { return HasComponent(ComponentIds.Ghost); } }

        static readonly Stack<GhostComponent> _ghostComponentPool = new Stack<GhostComponent>();

        public static void ClearGhostComponentPool() {
            _ghostComponentPool.Clear();
        }

        public Entity AddGhost(float newCurrentTime, float newSpawnTime, float newDuration) {
            var component = _ghostComponentPool.Count > 0 ? _ghostComponentPool.Pop() : new GhostComponent();
            component.currentTime = newCurrentTime;
            component.spawnTime = newSpawnTime;
            component.duration = newDuration;
            return AddComponent(ComponentIds.Ghost, component);
        }

        public Entity ReplaceGhost(float newCurrentTime, float newSpawnTime, float newDuration) {
            var previousComponent = hasGhost ? ghost : null;
            var component = _ghostComponentPool.Count > 0 ? _ghostComponentPool.Pop() : new GhostComponent();
            component.currentTime = newCurrentTime;
            component.spawnTime = newSpawnTime;
            component.duration = newDuration;
            ReplaceComponent(ComponentIds.Ghost, component);
            if (previousComponent != null) {
                _ghostComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveGhost() {
            var component = ghost;
            RemoveComponent(ComponentIds.Ghost);
            _ghostComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherGhost;

        public static IMatcher Ghost {
            get {
                if (_matcherGhost == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Ghost);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherGhost = matcher;
                }

                return _matcherGhost;
            }
        }
    }
}
