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
        static AllOfMatcher _matcherTest;

        public static AllOfMatcher Test {
            get {
                if (_matcherTest == null) {
                    _matcherTest = new Matcher(ComponentIds.Test);
                }

                return _matcherTest;
            }
        }
    }
}
