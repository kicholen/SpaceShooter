using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public GameStatsComponent gameStats { get { return (GameStatsComponent)GetComponent(ComponentIds.GameStats); } }

        public bool hasGameStats { get { return HasComponent(ComponentIds.GameStats); } }

        static readonly Stack<GameStatsComponent> _gameStatsComponentPool = new Stack<GameStatsComponent>();

        public static void ClearGameStatsComponentPool() {
            _gameStatsComponentPool.Clear();
        }

        public Entity AddGameStats(int newStarsPicked, int newBonusesPicked, int newShipsDestroyed) {
            var component = _gameStatsComponentPool.Count > 0 ? _gameStatsComponentPool.Pop() : new GameStatsComponent();
            component.starsPicked = newStarsPicked;
            component.bonusesPicked = newBonusesPicked;
            component.shipsDestroyed = newShipsDestroyed;
            return AddComponent(ComponentIds.GameStats, component);
        }

        public Entity ReplaceGameStats(int newStarsPicked, int newBonusesPicked, int newShipsDestroyed) {
            var previousComponent = hasGameStats ? gameStats : null;
            var component = _gameStatsComponentPool.Count > 0 ? _gameStatsComponentPool.Pop() : new GameStatsComponent();
            component.starsPicked = newStarsPicked;
            component.bonusesPicked = newBonusesPicked;
            component.shipsDestroyed = newShipsDestroyed;
            ReplaceComponent(ComponentIds.GameStats, component);
            if (previousComponent != null) {
                _gameStatsComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveGameStats() {
            var component = gameStats;
            RemoveComponent(ComponentIds.GameStats);
            _gameStatsComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherGameStats;

        public static IMatcher GameStats {
            get {
                if (_matcherGameStats == null) {
                    _matcherGameStats = Matcher.AllOf(ComponentIds.GameStats);
                }

                return _matcherGameStats;
            }
        }
    }
}
