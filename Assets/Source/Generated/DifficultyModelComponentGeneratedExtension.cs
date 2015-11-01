using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public DifficultyModelComponent difficultyModel { get { return (DifficultyModelComponent)GetComponent(ComponentIds.DifficultyModel); } }

        public bool hasDifficultyModel { get { return HasComponent(ComponentIds.DifficultyModel); } }

        static readonly Stack<DifficultyModelComponent> _difficultyModelComponentPool = new Stack<DifficultyModelComponent>();

        public static void ClearDifficultyModelComponentPool() {
            _difficultyModelComponentPool.Clear();
        }

        public Entity AddDifficultyModel(int newType, int newHpBoostPercent, int newDmgBoostPercent, int newMissileSpeedBoostPercent) {
            var component = _difficultyModelComponentPool.Count > 0 ? _difficultyModelComponentPool.Pop() : new DifficultyModelComponent();
            component.type = newType;
            component.hpBoostPercent = newHpBoostPercent;
            component.dmgBoostPercent = newDmgBoostPercent;
            component.missileSpeedBoostPercent = newMissileSpeedBoostPercent;
            return AddComponent(ComponentIds.DifficultyModel, component);
        }

        public Entity ReplaceDifficultyModel(int newType, int newHpBoostPercent, int newDmgBoostPercent, int newMissileSpeedBoostPercent) {
            var previousComponent = hasDifficultyModel ? difficultyModel : null;
            var component = _difficultyModelComponentPool.Count > 0 ? _difficultyModelComponentPool.Pop() : new DifficultyModelComponent();
            component.type = newType;
            component.hpBoostPercent = newHpBoostPercent;
            component.dmgBoostPercent = newDmgBoostPercent;
            component.missileSpeedBoostPercent = newMissileSpeedBoostPercent;
            ReplaceComponent(ComponentIds.DifficultyModel, component);
            if (previousComponent != null) {
                _difficultyModelComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveDifficultyModel() {
            var component = difficultyModel;
            RemoveComponent(ComponentIds.DifficultyModel);
            _difficultyModelComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherDifficultyModel;

        public static IMatcher DifficultyModel {
            get {
                if (_matcherDifficultyModel == null) {
                    _matcherDifficultyModel = Matcher.AllOf(ComponentIds.DifficultyModel);
                }

                return _matcherDifficultyModel;
            }
        }
    }
}
