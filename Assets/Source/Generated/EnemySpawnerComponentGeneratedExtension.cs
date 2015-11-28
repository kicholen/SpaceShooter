using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public EnemySpawnerComponent enemySpawner { get { return (EnemySpawnerComponent)GetComponent(ComponentIds.EnemySpawner); } }

        public bool hasEnemySpawner { get { return HasComponent(ComponentIds.EnemySpawner); } }

        static readonly Stack<EnemySpawnerComponent> _enemySpawnerComponentPool = new Stack<EnemySpawnerComponent>();

        public static void ClearEnemySpawnerComponentPool() {
            _enemySpawnerComponentPool.Clear();
        }

        public Entity AddEnemySpawner(int newLevel, bool newUsed, LevelModelComponent newModel) {
            var component = _enemySpawnerComponentPool.Count > 0 ? _enemySpawnerComponentPool.Pop() : new EnemySpawnerComponent();
            component.level = newLevel;
            component.used = newUsed;
            component.model = newModel;
            return AddComponent(ComponentIds.EnemySpawner, component);
        }

        public Entity ReplaceEnemySpawner(int newLevel, bool newUsed, LevelModelComponent newModel) {
            var previousComponent = hasEnemySpawner ? enemySpawner : null;
            var component = _enemySpawnerComponentPool.Count > 0 ? _enemySpawnerComponentPool.Pop() : new EnemySpawnerComponent();
            component.level = newLevel;
            component.used = newUsed;
            component.model = newModel;
            ReplaceComponent(ComponentIds.EnemySpawner, component);
            if (previousComponent != null) {
                _enemySpawnerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveEnemySpawner() {
            var component = enemySpawner;
            RemoveComponent(ComponentIds.EnemySpawner);
            _enemySpawnerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherEnemySpawner;

        public static IMatcher EnemySpawner {
            get {
                if (_matcherEnemySpawner == null) {
                    _matcherEnemySpawner = Matcher.AllOf(ComponentIds.EnemySpawner);
                }

                return _matcherEnemySpawner;
            }
        }
    }
}
