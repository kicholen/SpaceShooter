using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public DifficultyControllerComponent difficultyController { get { return (DifficultyControllerComponent)GetComponent(ComponentIds.DifficultyController); } }

        public bool hasDifficultyController { get { return HasComponent(ComponentIds.DifficultyController); } }

        static readonly Stack<DifficultyControllerComponent> _difficultyControllerComponentPool = new Stack<DifficultyControllerComponent>();

        public static void ClearDifficultyControllerComponentPool() {
            _difficultyControllerComponentPool.Clear();
        }

        public Entity AddDifficultyController(int newDifficultyType, int newHpBoostPercent, int newDmgBoostPercent, int newMissileSpeedBoostPercent) {
            var component = _difficultyControllerComponentPool.Count > 0 ? _difficultyControllerComponentPool.Pop() : new DifficultyControllerComponent();
            component.difficultyType = newDifficultyType;
            component.hpBoostPercent = newHpBoostPercent;
            component.dmgBoostPercent = newDmgBoostPercent;
            component.missileSpeedBoostPercent = newMissileSpeedBoostPercent;
            return AddComponent(ComponentIds.DifficultyController, component);
        }

        public Entity ReplaceDifficultyController(int newDifficultyType, int newHpBoostPercent, int newDmgBoostPercent, int newMissileSpeedBoostPercent) {
            var previousComponent = hasDifficultyController ? difficultyController : null;
            var component = _difficultyControllerComponentPool.Count > 0 ? _difficultyControllerComponentPool.Pop() : new DifficultyControllerComponent();
            component.difficultyType = newDifficultyType;
            component.hpBoostPercent = newHpBoostPercent;
            component.dmgBoostPercent = newDmgBoostPercent;
            component.missileSpeedBoostPercent = newMissileSpeedBoostPercent;
            ReplaceComponent(ComponentIds.DifficultyController, component);
            if (previousComponent != null) {
                _difficultyControllerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveDifficultyController() {
            var component = difficultyController;
            RemoveComponent(ComponentIds.DifficultyController);
            _difficultyControllerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherDifficultyController;

        public static AllOfMatcher DifficultyController {
            get {
                if (_matcherDifficultyController == null) {
                    _matcherDifficultyController = new Matcher(ComponentIds.DifficultyController);
                }

                return _matcherDifficultyController;
            }
        }
    }
}
