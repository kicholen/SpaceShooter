using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public EnemyFactoryComponent enemyFactory { get { return (EnemyFactoryComponent)GetComponent(ComponentIds.EnemyFactory); } }

        public bool hasEnemyFactory { get { return HasComponent(ComponentIds.EnemyFactory); } }

        static readonly Stack<EnemyFactoryComponent> _enemyFactoryComponentPool = new Stack<EnemyFactoryComponent>();

        public static void ClearEnemyFactoryComponentPool() {
            _enemyFactoryComponentPool.Clear();
        }

        public Entity AddEnemyFactory(EnemyFactory newFactory) {
            var component = _enemyFactoryComponentPool.Count > 0 ? _enemyFactoryComponentPool.Pop() : new EnemyFactoryComponent();
            component.factory = newFactory;
            return AddComponent(ComponentIds.EnemyFactory, component);
        }

        public Entity ReplaceEnemyFactory(EnemyFactory newFactory) {
            var previousComponent = hasEnemyFactory ? enemyFactory : null;
            var component = _enemyFactoryComponentPool.Count > 0 ? _enemyFactoryComponentPool.Pop() : new EnemyFactoryComponent();
            component.factory = newFactory;
            ReplaceComponent(ComponentIds.EnemyFactory, component);
            if (previousComponent != null) {
                _enemyFactoryComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveEnemyFactory() {
            var component = enemyFactory;
            RemoveComponent(ComponentIds.EnemyFactory);
            _enemyFactoryComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherEnemyFactory;

        public static IMatcher EnemyFactory {
            get {
                if (_matcherEnemyFactory == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.EnemyFactory);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherEnemyFactory = matcher;
                }

                return _matcherEnemyFactory;
            }
        }
    }
}
