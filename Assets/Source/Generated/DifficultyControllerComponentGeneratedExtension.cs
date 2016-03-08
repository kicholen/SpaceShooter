//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {
    public partial class Entity {
        public DifficultyControllerComponent difficultyController { get { return (DifficultyControllerComponent)GetComponent(ComponentIds.DifficultyController); } }

        public bool hasDifficultyController { get { return HasComponent(ComponentIds.DifficultyController); } }

        public Entity AddDifficultyController(int newDifficultyType, int newHpBoostPercent, int newDmgBoostPercent, int newMissileSpeedBoostPercent) {
            var component = CreateComponent<DifficultyControllerComponent>(ComponentIds.DifficultyController);
            component.difficultyType = newDifficultyType;
            component.hpBoostPercent = newHpBoostPercent;
            component.dmgBoostPercent = newDmgBoostPercent;
            component.missileSpeedBoostPercent = newMissileSpeedBoostPercent;
            return AddComponent(ComponentIds.DifficultyController, component);
        }

        public Entity ReplaceDifficultyController(int newDifficultyType, int newHpBoostPercent, int newDmgBoostPercent, int newMissileSpeedBoostPercent) {
            var component = CreateComponent<DifficultyControllerComponent>(ComponentIds.DifficultyController);
            component.difficultyType = newDifficultyType;
            component.hpBoostPercent = newHpBoostPercent;
            component.dmgBoostPercent = newDmgBoostPercent;
            component.missileSpeedBoostPercent = newMissileSpeedBoostPercent;
            ReplaceComponent(ComponentIds.DifficultyController, component);
            return this;
        }

        public Entity RemoveDifficultyController() {
            return RemoveComponent(ComponentIds.DifficultyController);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherDifficultyController;

        public static IMatcher DifficultyController {
            get {
                if (_matcherDifficultyController == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.DifficultyController);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherDifficultyController = matcher;
                }

                return _matcherDifficultyController;
            }
        }
    }
}
