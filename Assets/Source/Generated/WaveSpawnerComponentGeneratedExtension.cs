using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public WaveSpawnerComponent waveSpawner { get { return (WaveSpawnerComponent)GetComponent(ComponentIds.WaveSpawner); } }

        public bool hasWaveSpawner { get { return HasComponent(ComponentIds.WaveSpawner); } }

        static readonly Stack<WaveSpawnerComponent> _waveSpawnerComponentPool = new Stack<WaveSpawnerComponent>();

        public static void ClearWaveSpawnerComponentPool() {
            _waveSpawnerComponentPool.Clear();
        }

        public Entity AddWaveSpawner(int newCount, int newType, float newTimeOffset, float newTime, float newSpeed, int newHealth, int newPath, int newGrid, int newDamage) {
            var component = _waveSpawnerComponentPool.Count > 0 ? _waveSpawnerComponentPool.Pop() : new WaveSpawnerComponent();
            component.count = newCount;
            component.type = newType;
            component.timeOffset = newTimeOffset;
            component.time = newTime;
            component.speed = newSpeed;
            component.health = newHealth;
            component.path = newPath;
            component.grid = newGrid;
            component.damage = newDamage;
            return AddComponent(ComponentIds.WaveSpawner, component);
        }

        public Entity ReplaceWaveSpawner(int newCount, int newType, float newTimeOffset, float newTime, float newSpeed, int newHealth, int newPath, int newGrid, int newDamage) {
            var previousComponent = hasWaveSpawner ? waveSpawner : null;
            var component = _waveSpawnerComponentPool.Count > 0 ? _waveSpawnerComponentPool.Pop() : new WaveSpawnerComponent();
            component.count = newCount;
            component.type = newType;
            component.timeOffset = newTimeOffset;
            component.time = newTime;
            component.speed = newSpeed;
            component.health = newHealth;
            component.path = newPath;
            component.grid = newGrid;
            component.damage = newDamage;
            ReplaceComponent(ComponentIds.WaveSpawner, component);
            if (previousComponent != null) {
                _waveSpawnerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveWaveSpawner() {
            var component = waveSpawner;
            RemoveComponent(ComponentIds.WaveSpawner);
            _waveSpawnerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherWaveSpawner;

        public static IMatcher WaveSpawner {
            get {
                if (_matcherWaveSpawner == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.WaveSpawner);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherWaveSpawner = matcher;
                }

                return _matcherWaveSpawner;
            }
        }
    }
}
