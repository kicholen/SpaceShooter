using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SpeedBonusComponent speedBonus { get { return (SpeedBonusComponent)GetComponent(ComponentIds.SpeedBonus); } }

        public bool hasSpeedBonus { get { return HasComponent(ComponentIds.SpeedBonus); } }

        static readonly Stack<SpeedBonusComponent> _speedBonusComponentPool = new Stack<SpeedBonusComponent>();

        public static void ClearSpeedBonusComponentPool() {
            _speedBonusComponentPool.Clear();
        }

        public Entity AddSpeedBonus(float newVelocity, float newSavedVelocity, float newTime) {
            var component = _speedBonusComponentPool.Count > 0 ? _speedBonusComponentPool.Pop() : new SpeedBonusComponent();
            component.velocity = newVelocity;
            component.savedVelocity = newSavedVelocity;
            component.time = newTime;
            return AddComponent(ComponentIds.SpeedBonus, component);
        }

        public Entity ReplaceSpeedBonus(float newVelocity, float newSavedVelocity, float newTime) {
            var previousComponent = hasSpeedBonus ? speedBonus : null;
            var component = _speedBonusComponentPool.Count > 0 ? _speedBonusComponentPool.Pop() : new SpeedBonusComponent();
            component.velocity = newVelocity;
            component.savedVelocity = newSavedVelocity;
            component.time = newTime;
            ReplaceComponent(ComponentIds.SpeedBonus, component);
            if (previousComponent != null) {
                _speedBonusComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSpeedBonus() {
            var component = speedBonus;
            RemoveComponent(ComponentIds.SpeedBonus);
            _speedBonusComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSpeedBonus;

        public static IMatcher SpeedBonus {
            get {
                if (_matcherSpeedBonus == null) {
                    _matcherSpeedBonus = Matcher.AllOf(ComponentIds.SpeedBonus);
                }

                return _matcherSpeedBonus;
            }
        }
    }
}
