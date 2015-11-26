using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public IndicatorComponent indicator { get { return (IndicatorComponent)GetComponent(ComponentIds.Indicator); } }

        public bool hasIndicator { get { return HasComponent(ComponentIds.Indicator); } }

        static readonly Stack<IndicatorComponent> _indicatorComponentPool = new Stack<IndicatorComponent>();

        public static void ClearIndicatorComponentPool() {
            _indicatorComponentPool.Clear();
        }

        public Entity AddIndicator(float newCurrentValue, float newTotalValue, string newType) {
            var component = _indicatorComponentPool.Count > 0 ? _indicatorComponentPool.Pop() : new IndicatorComponent();
            component.currentValue = newCurrentValue;
            component.totalValue = newTotalValue;
            component.type = newType;
            return AddComponent(ComponentIds.Indicator, component);
        }

        public Entity ReplaceIndicator(float newCurrentValue, float newTotalValue, string newType) {
            var previousComponent = hasIndicator ? indicator : null;
            var component = _indicatorComponentPool.Count > 0 ? _indicatorComponentPool.Pop() : new IndicatorComponent();
            component.currentValue = newCurrentValue;
            component.totalValue = newTotalValue;
            component.type = newType;
            ReplaceComponent(ComponentIds.Indicator, component);
            if (previousComponent != null) {
                _indicatorComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveIndicator() {
            var component = indicator;
            RemoveComponent(ComponentIds.Indicator);
            _indicatorComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherIndicator;

        public static IMatcher Indicator {
            get {
                if (_matcherIndicator == null) {
                    _matcherIndicator = Matcher.AllOf(ComponentIds.Indicator);
                }

                return _matcherIndicator;
            }
        }
    }
}
