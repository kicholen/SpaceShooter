using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CameraComponent camera { get { return (CameraComponent)GetComponent(ComponentIds.Camera); } }

        public bool hasCamera { get { return HasComponent(ComponentIds.Camera); } }

        static readonly Stack<CameraComponent> _cameraComponentPool = new Stack<CameraComponent>();

        public static void ClearCameraComponentPool() {
            _cameraComponentPool.Clear();
        }

        public Entity AddCamera(UnityEngine.Camera newCamera, Entitas.Entity newFollow) {
            var component = _cameraComponentPool.Count > 0 ? _cameraComponentPool.Pop() : new CameraComponent();
            component.camera = newCamera;
            component.follow = newFollow;
            return AddComponent(ComponentIds.Camera, component);
        }

        public Entity ReplaceCamera(UnityEngine.Camera newCamera, Entitas.Entity newFollow) {
            var previousComponent = hasCamera ? camera : null;
            var component = _cameraComponentPool.Count > 0 ? _cameraComponentPool.Pop() : new CameraComponent();
            component.camera = newCamera;
            component.follow = newFollow;
            ReplaceComponent(ComponentIds.Camera, component);
            if (previousComponent != null) {
                _cameraComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCamera() {
            var component = camera;
            RemoveComponent(ComponentIds.Camera);
            _cameraComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherCamera;

        public static AllOfMatcher Camera {
            get {
                if (_matcherCamera == null) {
                    _matcherCamera = new Matcher(ComponentIds.Camera);
                }

                return _matcherCamera;
            }
        }
    }
}
