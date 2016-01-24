using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CreateCameraComponent createCamera { get { return (CreateCameraComponent)GetComponent(ComponentIds.CreateCamera); } }

        public bool hasCreateCamera { get { return HasComponent(ComponentIds.CreateCamera); } }

        static readonly Stack<CreateCameraComponent> _createCameraComponentPool = new Stack<CreateCameraComponent>();

        public static void ClearCreateCameraComponentPool() {
            _createCameraComponentPool.Clear();
        }

        public Entity AddCreateCamera(int newType, bool newShouldReset) {
            var component = _createCameraComponentPool.Count > 0 ? _createCameraComponentPool.Pop() : new CreateCameraComponent();
            component.type = newType;
            component.shouldReset = newShouldReset;
            return AddComponent(ComponentIds.CreateCamera, component);
        }

        public Entity ReplaceCreateCamera(int newType, bool newShouldReset) {
            var previousComponent = hasCreateCamera ? createCamera : null;
            var component = _createCameraComponentPool.Count > 0 ? _createCameraComponentPool.Pop() : new CreateCameraComponent();
            component.type = newType;
            component.shouldReset = newShouldReset;
            ReplaceComponent(ComponentIds.CreateCamera, component);
            if (previousComponent != null) {
                _createCameraComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCreateCamera() {
            var component = createCamera;
            RemoveComponent(ComponentIds.CreateCamera);
            _createCameraComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCreateCamera;

        public static IMatcher CreateCamera {
            get {
                if (_matcherCreateCamera == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CreateCamera);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCreateCamera = matcher;
                }

                return _matcherCreateCamera;
            }
        }
    }
}
