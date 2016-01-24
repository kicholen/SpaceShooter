using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public FaceDirectionComponent faceDirection { get { return (FaceDirectionComponent)GetComponent(ComponentIds.FaceDirection); } }

        public bool hasFaceDirection { get { return HasComponent(ComponentIds.FaceDirection); } }

        static readonly Stack<FaceDirectionComponent> _faceDirectionComponentPool = new Stack<FaceDirectionComponent>();

        public static void ClearFaceDirectionComponentPool() {
            _faceDirectionComponentPool.Clear();
        }

        public Entity AddFaceDirection(bool newShouldUpdate) {
            var component = _faceDirectionComponentPool.Count > 0 ? _faceDirectionComponentPool.Pop() : new FaceDirectionComponent();
            component.shouldUpdate = newShouldUpdate;
            return AddComponent(ComponentIds.FaceDirection, component);
        }

        public Entity ReplaceFaceDirection(bool newShouldUpdate) {
            var previousComponent = hasFaceDirection ? faceDirection : null;
            var component = _faceDirectionComponentPool.Count > 0 ? _faceDirectionComponentPool.Pop() : new FaceDirectionComponent();
            component.shouldUpdate = newShouldUpdate;
            ReplaceComponent(ComponentIds.FaceDirection, component);
            if (previousComponent != null) {
                _faceDirectionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveFaceDirection() {
            var component = faceDirection;
            RemoveComponent(ComponentIds.FaceDirection);
            _faceDirectionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherFaceDirection;

        public static IMatcher FaceDirection {
            get {
                if (_matcherFaceDirection == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.FaceDirection);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherFaceDirection = matcher;
                }

                return _matcherFaceDirection;
            }
        }
    }
}
