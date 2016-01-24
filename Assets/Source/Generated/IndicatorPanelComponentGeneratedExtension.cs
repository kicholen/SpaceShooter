namespace Entitas {
    public partial class Entity {
        static readonly IndicatorPanelComponent indicatorPanelComponent = new IndicatorPanelComponent();

        public bool isIndicatorPanel {
            get { return HasComponent(ComponentIds.IndicatorPanel); }
            set {
                if (value != isIndicatorPanel) {
                    if (value) {
                        AddComponent(ComponentIds.IndicatorPanel, indicatorPanelComponent);
                    } else {
                        RemoveComponent(ComponentIds.IndicatorPanel);
                    }
                }
            }
        }

        public Entity IsIndicatorPanel(bool value) {
            isIndicatorPanel = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherIndicatorPanel;

        public static IMatcher IndicatorPanel {
            get {
                if (_matcherIndicatorPanel == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.IndicatorPanel);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherIndicatorPanel = matcher;
                }

                return _matcherIndicatorPanel;
            }
        }
    }
}
