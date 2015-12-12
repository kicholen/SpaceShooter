using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public DefaultCameraComponent defaultCamera { get { return (DefaultCameraComponent)GetComponent(ComponentIds.DefaultCamera); } }

        public bool hasDefaultCamera { get { return HasComponent(ComponentIds.DefaultCamera); } }

        static readonly Stack<DefaultCameraComponent> _defaultCameraComponentPool = new Stack<DefaultCameraComponent>();

        public static void ClearDefaultCameraComponentPool() {
            _defaultCameraComponentPool.Clear();
        }

        public Entity AddDefaultCamera(UnityEngine.Vector3 newOffset) {
            var component = _defaultCameraComponentPool.Count > 0 ? _defaultCameraComponentPool.Pop() : new DefaultCameraComponent();
            component.offset = newOffset;
            return AddComponent(ComponentIds.DefaultCamera, component);
        }

        public Entity ReplaceDefaultCamera(UnityEngine.Vector3 newOffset) {
            var previousComponent = hasDefaultCamera ? defaultCamera : null;
            var component = _defaultCameraComponentPool.Count > 0 ? _defaultCameraComponentPool.Pop() : new DefaultCameraComponent();
            component.offset = newOffset;
            ReplaceComponent(ComponentIds.DefaultCamera, component);
            if (previousComponent != null) {
                _defaultCameraComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveDefaultCamera() {
            var component = defaultCamera;
            RemoveComponent(ComponentIds.DefaultCamera);
            _defaultCameraComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherDefaultCamera;

        public static IMatcher DefaultCamera {
            get {
                if (_matcherDefaultCamera == null) {
                    _matcherDefaultCamera = Matcher.AllOf(ComponentIds.DefaultCamera);
                }

                return _matcherDefaultCamera;
            }
        }
    }
}
