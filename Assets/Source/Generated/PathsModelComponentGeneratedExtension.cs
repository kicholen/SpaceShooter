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
        public PathsModelComponent pathsModel { get { return (PathsModelComponent)GetComponent(ComponentIds.PathsModel); } }

        public bool hasPathsModel { get { return HasComponent(ComponentIds.PathsModel); } }

        public Entity AddPathsModel(System.Collections.Generic.Dictionary<string, PathModel> newMap) {
            var component = CreateComponent<PathsModelComponent>(ComponentIds.PathsModel);
            component.map = newMap;
            return AddComponent(ComponentIds.PathsModel, component);
        }

        public Entity ReplacePathsModel(System.Collections.Generic.Dictionary<string, PathModel> newMap) {
            var component = CreateComponent<PathsModelComponent>(ComponentIds.PathsModel);
            component.map = newMap;
            ReplaceComponent(ComponentIds.PathsModel, component);
            return this;
        }

        public Entity RemovePathsModel() {
            return RemoveComponent(ComponentIds.PathsModel);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPathsModel;

        public static IMatcher PathsModel {
            get {
                if (_matcherPathsModel == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.PathsModel);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherPathsModel = matcher;
                }

                return _matcherPathsModel;
            }
        }
    }
}
