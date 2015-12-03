using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SortOrderComponent sortOrder { get { return (SortOrderComponent)GetComponent(ComponentIds.SortOrder); } }

        public bool hasSortOrder { get { return HasComponent(ComponentIds.SortOrder); } }

        static readonly Stack<SortOrderComponent> _sortOrderComponentPool = new Stack<SortOrderComponent>();

        public static void ClearSortOrderComponentPool() {
            _sortOrderComponentPool.Clear();
        }

        public Entity AddSortOrder(float newZ) {
            var component = _sortOrderComponentPool.Count > 0 ? _sortOrderComponentPool.Pop() : new SortOrderComponent();
            component.z = newZ;
            return AddComponent(ComponentIds.SortOrder, component);
        }

        public Entity ReplaceSortOrder(float newZ) {
            var previousComponent = hasSortOrder ? sortOrder : null;
            var component = _sortOrderComponentPool.Count > 0 ? _sortOrderComponentPool.Pop() : new SortOrderComponent();
            component.z = newZ;
            ReplaceComponent(ComponentIds.SortOrder, component);
            if (previousComponent != null) {
                _sortOrderComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSortOrder() {
            var component = sortOrder;
            RemoveComponent(ComponentIds.SortOrder);
            _sortOrderComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSortOrder;

        public static IMatcher SortOrder {
            get {
                if (_matcherSortOrder == null) {
                    _matcherSortOrder = Matcher.AllOf(ComponentIds.SortOrder);
                }

                return _matcherSortOrder;
            }
        }
    }
}
