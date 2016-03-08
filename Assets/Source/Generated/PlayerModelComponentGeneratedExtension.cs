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
        public PlayerModelComponent playerModel { get { return (PlayerModelComponent)GetComponent(ComponentIds.PlayerModel); } }

        public bool hasPlayerModel { get { return HasComponent(ComponentIds.PlayerModel); } }

        public Entity AddPlayerModel(string newName) {
            var component = CreateComponent<PlayerModelComponent>(ComponentIds.PlayerModel);
            component.name = newName;
            return AddComponent(ComponentIds.PlayerModel, component);
        }

        public Entity ReplacePlayerModel(string newName) {
            var component = CreateComponent<PlayerModelComponent>(ComponentIds.PlayerModel);
            component.name = newName;
            ReplaceComponent(ComponentIds.PlayerModel, component);
            return this;
        }

        public Entity RemovePlayerModel() {
            return RemoveComponent(ComponentIds.PlayerModel);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPlayerModel;

        public static IMatcher PlayerModel {
            get {
                if (_matcherPlayerModel == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.PlayerModel);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherPlayerModel = matcher;
                }

                return _matcherPlayerModel;
            }
        }
    }
}
