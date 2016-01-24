namespace Entitas {
    public partial class Entity {
        static readonly TestComponent testComponent = new TestComponent();

        public bool isTest {
            get { return HasComponent(ComponentIds.Test); }
            set {
                if (value != isTest) {
                    if (value) {
                        AddComponent(ComponentIds.Test, testComponent);
                    } else {
                        RemoveComponent(ComponentIds.Test);
                    }
                }
            }
        }

        public Entity IsTest(bool value) {
            isTest = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTest;

        public static IMatcher Test {
            get {
                if (_matcherTest == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Test);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTest = matcher;
                }

                return _matcherTest;
            }
        }
    }
}
