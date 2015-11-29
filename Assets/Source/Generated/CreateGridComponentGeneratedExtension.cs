namespace Entitas {
    public partial class Entity {
        static readonly CreateGridComponent createGridComponent = new CreateGridComponent();

        public bool isCreateGrid {
            get { return HasComponent(ComponentIds.CreateGrid); }
            set {
                if (value != isCreateGrid) {
                    if (value) {
                        AddComponent(ComponentIds.CreateGrid, createGridComponent);
                    } else {
                        RemoveComponent(ComponentIds.CreateGrid);
                    }
                }
            }
        }

        public Entity IsCreateGrid(bool value) {
            isCreateGrid = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCreateGrid;

        public static IMatcher CreateGrid {
            get {
                if (_matcherCreateGrid == null) {
                    _matcherCreateGrid = Matcher.AllOf(ComponentIds.CreateGrid);
                }

                return _matcherCreateGrid;
            }
        }
    }
}
