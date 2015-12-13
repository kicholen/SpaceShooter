namespace Entitas {
    public partial class Entity {
        static readonly LeaderFollowerComponent leaderFollowerComponent = new LeaderFollowerComponent();

        public bool isLeaderFollower {
            get { return HasComponent(ComponentIds.LeaderFollower); }
            set {
                if (value != isLeaderFollower) {
                    if (value) {
                        AddComponent(ComponentIds.LeaderFollower, leaderFollowerComponent);
                    } else {
                        RemoveComponent(ComponentIds.LeaderFollower);
                    }
                }
            }
        }

        public Entity IsLeaderFollower(bool value) {
            isLeaderFollower = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherLeaderFollower;

        public static IMatcher LeaderFollower {
            get {
                if (_matcherLeaderFollower == null) {
                    _matcherLeaderFollower = Matcher.AllOf(ComponentIds.LeaderFollower);
                }

                return _matcherLeaderFollower;
            }
        }
    }
}
