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
        public ScoreComponent score { get { return (ScoreComponent)GetComponent(ComponentIds.Score); } }

        public bool hasScore { get { return HasComponent(ComponentIds.Score); } }

        public Entity AddScore(int newScore, int newMultiplierCount, float newMultiplier, float newTime, float newMultiplierDuration) {
            var component = CreateComponent<ScoreComponent>(ComponentIds.Score);
            component.score = newScore;
            component.multiplierCount = newMultiplierCount;
            component.multiplier = newMultiplier;
            component.time = newTime;
            component.multiplierDuration = newMultiplierDuration;
            return AddComponent(ComponentIds.Score, component);
        }

        public Entity ReplaceScore(int newScore, int newMultiplierCount, float newMultiplier, float newTime, float newMultiplierDuration) {
            var component = CreateComponent<ScoreComponent>(ComponentIds.Score);
            component.score = newScore;
            component.multiplierCount = newMultiplierCount;
            component.multiplier = newMultiplier;
            component.time = newTime;
            component.multiplierDuration = newMultiplierDuration;
            ReplaceComponent(ComponentIds.Score, component);
            return this;
        }

        public Entity RemoveScore() {
            return RemoveComponent(ComponentIds.Score);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherScore;

        public static IMatcher Score {
            get {
                if (_matcherScore == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Score);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherScore = matcher;
                }

                return _matcherScore;
            }
        }
    }
}
