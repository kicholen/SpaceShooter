using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public BonusSpawnerComponent bonusSpawner { get { return (BonusSpawnerComponent)GetComponent(ComponentIds.BonusSpawner); } }

        public bool hasBonusSpawner { get { return HasComponent(ComponentIds.BonusSpawner); } }

        static readonly Stack<BonusSpawnerComponent> _bonusSpawnerComponentPool = new Stack<BonusSpawnerComponent>();

        public static void ClearBonusSpawnerComponentPool() {
            _bonusSpawnerComponentPool.Clear();
        }

        public Entity AddBonusSpawner(int newType) {
            var component = _bonusSpawnerComponentPool.Count > 0 ? _bonusSpawnerComponentPool.Pop() : new BonusSpawnerComponent();
            component.type = newType;
            return AddComponent(ComponentIds.BonusSpawner, component);
        }

        public Entity ReplaceBonusSpawner(int newType) {
            var previousComponent = hasBonusSpawner ? bonusSpawner : null;
            var component = _bonusSpawnerComponentPool.Count > 0 ? _bonusSpawnerComponentPool.Pop() : new BonusSpawnerComponent();
            component.type = newType;
            ReplaceComponent(ComponentIds.BonusSpawner, component);
            if (previousComponent != null) {
                _bonusSpawnerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveBonusSpawner() {
            var component = bonusSpawner;
            RemoveComponent(ComponentIds.BonusSpawner);
            _bonusSpawnerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherBonusSpawner;

        public static AllOfMatcher BonusSpawner {
            get {
                if (_matcherBonusSpawner == null) {
                    _matcherBonusSpawner = new Matcher(ComponentIds.BonusSpawner);
                }

                return _matcherBonusSpawner;
            }
        }
    }
}
