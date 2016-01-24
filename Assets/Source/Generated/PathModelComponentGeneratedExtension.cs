using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public PathModelComponent pathModel { get { return (PathModelComponent)GetComponent(ComponentIds.PathModel); } }

        public bool hasPathModel { get { return HasComponent(ComponentIds.PathModel); } }

        static readonly Stack<PathModelComponent> _pathModelComponentPool = new Stack<PathModelComponent>();

        public static void ClearPathModelComponentPool() {
            _pathModelComponentPool.Clear();
        }

        public Entity AddPathModel(long newId, string newName, System.Collections.Generic.List<UnityEngine.Vector2> newPoints) {
            var component = _pathModelComponentPool.Count > 0 ? _pathModelComponentPool.Pop() : new PathModelComponent();
            component.id = newId;
            component.name = newName;
            component.points = newPoints;
            return AddComponent(ComponentIds.PathModel, component);
        }

        public Entity ReplacePathModel(long newId, string newName, System.Collections.Generic.List<UnityEngine.Vector2> newPoints) {
            var previousComponent = hasPathModel ? pathModel : null;
            var component = _pathModelComponentPool.Count > 0 ? _pathModelComponentPool.Pop() : new PathModelComponent();
            component.id = newId;
            component.name = newName;
            component.points = newPoints;
            ReplaceComponent(ComponentIds.PathModel, component);
            if (previousComponent != null) {
                _pathModelComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePathModel() {
            var component = pathModel;
            RemoveComponent(ComponentIds.PathModel);
            _pathModelComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPathModel;

        public static IMatcher PathModel {
            get {
                if (_matcherPathModel == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.PathModel);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherPathModel = matcher;
                }

                return _matcherPathModel;
            }
        }
    }
}
