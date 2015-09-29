using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public EnemyComponent enemy { get { return (EnemyComponent)GetComponent(ComponentIds.Enemy); } }

        public bool hasEnemy { get { return HasComponent(ComponentIds.Enemy); } }

        static readonly Stack<EnemyComponent> _enemyComponentPool = new Stack<EnemyComponent>();

        public static void ClearEnemyComponentPool() {
            _enemyComponentPool.Clear();
        }

        public Entity AddEnemy(int newType) {
            var component = _enemyComponentPool.Count > 0 ? _enemyComponentPool.Pop() : new EnemyComponent();
            component.type = newType;
            return AddComponent(ComponentIds.Enemy, component);
        }

        public Entity ReplaceEnemy(int newType) {
            var previousComponent = hasEnemy ? enemy : null;
            var component = _enemyComponentPool.Count > 0 ? _enemyComponentPool.Pop() : new EnemyComponent();
            component.type = newType;
            ReplaceComponent(ComponentIds.Enemy, component);
            if (previousComponent != null) {
                _enemyComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveEnemy() {
            var component = enemy;
            RemoveComponent(ComponentIds.Enemy);
            _enemyComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherEnemy;

        public static AllOfMatcher Enemy {
            get {
                if (_matcherEnemy == null) {
                    _matcherEnemy = new Matcher(ComponentIds.Enemy);
                }

                return _matcherEnemy;
            }
        }
    }
}
