using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SpeedBonusComponent speedBonus { get { return (SpeedBonusComponent)GetComponent(ComponentIds.SpeedBonus); } }

        public bool hasSpeedBonus { get { return HasComponent(ComponentIds.SpeedBonus); } }

        static readonly Stack<SpeedBonusComponent> _speedBonusComponentPool = new Stack<SpeedBonusComponent>();

        public static void ClearSpeedBonusComponentPool() {
            _speedBonusComponentPool.Clear();
        }

        public Entity AddSpeedBonus(float newVelocityX, float newVelocityY, float newTime) {
            var component = _speedBonusComponentPool.Count > 0 ? _speedBonusComponentPool.Pop() : new SpeedBonusComponent();
            component.velocityX = newVelocityX;
            component.velocityY = newVelocityY;
            component.time = newTime;
            return AddComponent(ComponentIds.SpeedBonus, component);
        }

        public Entity ReplaceSpeedBonus(float newVelocityX, float newVelocityY, float newTime) {
            var previousComponent = hasSpeedBonus ? speedBonus : null;
            var component = _speedBonusComponentPool.Count > 0 ? _speedBonusComponentPool.Pop() : new SpeedBonusComponent();
            component.velocityX = newVelocityX;
            component.velocityY = newVelocityY;
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
        static AllOfMatcher _matcherSpeedBonus;

        public static AllOfMatcher SpeedBonus {
            get {
                if (_matcherSpeedBonus == null) {
                    _matcherSpeedBonus = new Matcher(ComponentIds.SpeedBonus);
                }

                return _matcherSpeedBonus;
            }
        }
    }
}